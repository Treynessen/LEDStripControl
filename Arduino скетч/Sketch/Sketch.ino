#include <FastLED.h>

#define VERTICAL_LEDS_NUM 8 // количество светодиодов на 1 вертикальной полосе
#define HORIZONTAL_LEDS_NUM 15 // количество светодиодов на 1 горизонтальной полосе
#define NUM_LEDS 2 * (VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM) // общее количество светодиодов
#define LED_CONTROL_PIN 6 // управляющий пин (с которого подается сигнал на ленту)
#define RELAY_POWER 2 // пин, питающий реле
#define RELAY_SIGNAL 3 // пин, управляющий реле
#define ARDUINO_BUFFER_SIZE 40

enum Modes {
	Connected,
	StaticColor,
	Animation,
	Ambilight,
	PolishFlag,
	NotConnected
};

byte mode = Modes::NotConnected;

unsigned long time_als = 0; // time after last signal

							// анимация
CRGBPalette16 currentPalette = RainbowColors_p;
TBlendType currentBlending = LINEARBLEND;
byte startIndex = 0;

CRGB leds[NUM_LEDS]; // лента

byte static_color[3]; // цвет для StaticColor режима

void setup() {
	Serial.begin(1000000);
	FastLED.addLeds<NEOPIXEL, LED_CONTROL_PIN>(leds, (2 * VERTICAL_LEDS_NUM + 2 * HORIZONTAL_LEDS_NUM));
	pinMode(RELAY_POWER, OUTPUT);
	digitalWrite(RELAY_POWER, HIGH);
	pinMode(RELAY_SIGNAL, OUTPUT);
	RelayOn(false);
}

void loop() {
	if (mode == Modes::NotConnected) Serial.write("LED Strip\n");

	if (Serial.available() > 0) {
		if (mode == Modes::NotConnected || millis() - time_als > 800) {
			mode = Serial.read();
			if (mode != Modes::NotConnected && mode != Modes::Connected) RelayOn(true);
			time_als = millis();
		}

		if (mode == Modes::Ambilight) {
			byte it_num = Rounding(3 * NUM_LEDS / (float)ARDUINO_BUFFER_SIZE); // количество частей с данными
			byte rgb = 0, led_num = 0;

			bool done = true;
			bool stop = false;

			while (!stop) {
				for (byte i = 0; i < it_num; ++i) {
					if (done) {
						time_als = millis();
						// отправляем ОК на ПК до тех пор пока не выйдет время или с ПК не будут отправлены данные
						while (Serial.available() == 0 && millis() - time_als < 1700) Serial.write("OK\n");
						if (Serial.available() == 0) {
							stop = true;
							break;
						}
						time_als = millis();
						if (i != it_num - 1) {
							for (int j = i * ARDUINO_BUFFER_SIZE; j < (i + 1) * ARDUINO_BUFFER_SIZE; ++j) {
								while (Serial.available() == 0 && millis() - time_als < 50);
								if (Serial.available() == 0) {
									stop = true;
									done = false;
									break;
								}

								if (rgb == 0) leds[led_num].r = Serial.read();
								else if (rgb == 1) leds[led_num].g = Serial.read();
								else if (rgb == 2) leds[led_num++].b = Serial.read();
								++rgb;
								if (rgb >= 3) rgb = 0;

								time_als = millis();
							}
						}
						else {
							for (int j = i * ARDUINO_BUFFER_SIZE; j < 3 * NUM_LEDS; ++j) {
								while (Serial.available() == 0 && millis() - time_als < 50);
								if (Serial.available() == 0) {
									stop = true;
									done = false;
									break;
								}

								if (rgb == 0) leds[led_num].r = Serial.read();
								else if (rgb == 1) leds[led_num].g = Serial.read();
								else if (rgb == 2) leds[led_num++].b = Serial.read();
								++rgb;
								if (rgb >= 3) rgb = 0;

								time_als = millis();
							}
						}
					}
					else break;
				}
				FastLED.show();
				rgb = 0;
				led_num = 0;
			}
		}
		else if (mode == Modes::StaticColor) {
			byte static_R = static_color[0];
			byte static_G = static_color[1];
			byte static_B = static_color[2];
			long temp_time = millis();
			for (byte i = 0; i < 3; ++i) {
				while (Serial.available() < 1 && millis() - temp_time < 10);
				if (Serial.available() > 0) {
					static_color[i] = Serial.read();
					temp_time = millis();
				}
				else {
					static_color[0] = static_R;
					static_color[1] = static_G;
					static_color[2] = static_B;
					break;
				}
			}
			StaticColorMode();
		}
		else if (mode == Modes::PolishFlag) PolishFlagMode();
		else if (mode == Modes::NotConnected) RelayOn(false);
	}

	if (mode == Modes::Animation) AnimationMode();
}

void RelayOn(bool on) {
	if (on) digitalWrite(RELAY_SIGNAL, HIGH);
	else digitalWrite(RELAY_SIGNAL, LOW);
}

void StaticColorMode() {
	for (int i = 0; i < NUM_LEDS; ++i) {
		leds[i].setRGB(static_color[0], static_color[1], static_color[2]);
	}
	FastLED.show();
}

void PolishFlagMode() {
	for (int i = 0; i < VERTICAL_LEDS_NUM / 2; ++i) {
		leds[i].setRGB(255, 0, 0);
		leds[2 * VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM - 1 - i].setRGB(255, 0, 0);
	}
	for (int i = VERTICAL_LEDS_NUM / 2; i < VERTICAL_LEDS_NUM; ++i) {
		leds[i].setRGB(255, 255, 255);
		leds[2 * VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM - 1 - i].setRGB(255, 255, 255);
	}
	for (int i = VERTICAL_LEDS_NUM; i < VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM; ++i) {
		leds[i].setRGB(255, 255, 255);
		leds[VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM + i].setRGB(255, 0, 0);
	}
	FastLED.show();
}

void AnimationMode() {
	++startIndex;

	byte colorIndex = startIndex;
	for (int i = 0; i < NUM_LEDS; ++i) {
		leds[i] = ColorFromPalette(currentPalette, colorIndex, 255, currentBlending); // 255 - яркость
		colorIndex += 1;
	}

	FastLED.delay(1000 / 60);
}

byte Rounding(float val) {
	int t_val = val;
	return t_val == val ? t_val : t_val + 1;
}