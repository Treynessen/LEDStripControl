#include <FastLED.h>

#define VERTICAL_LEDS_NUM 8 // ���������� ����������� �� 1 ������������ ������
#define HORIZONTAL_LEDS_NUM 15 // ���������� ����������� �� 1 �������������� ������
#define NUM_LEDS (2 * VERTICAL_LEDS_NUM + 2 * HORIZONTAL_LEDS_NUM) // ����� ���������� �����������
#define LED_CONTROL_PIN 6 // ����������� ��� (� �������� �������� ������ �� �����)
#define RELAY_POWER 2 // ���, �������� ����
#define RELAY_SIGNAL 3 // ���, ����������� ����

enum Modes {
	StaticColor,
	Animation,
	Ambilight,
	PolishFlag,
	NotConnected
};

byte mode = Modes::NotConnected;

unsigned long time_als = 0; // time after last signal

// ��������
CRGBPalette16 currentPalette = RainbowColors_p;
TBlendType currentBlending = LINEARBLEND;
byte startIndex = 0;

CRGB leds[NUM_LEDS]; // �����

byte static_R = 0, static_G = 0, static_B = 0; // ���� ��� StaticColor ������

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
		if (millis() - time_als > 800) {
			mode = Serial.read();
			if (mode != Modes::NotConnected) RelayOn(true);
			time_als = millis();
		}

		if (mode == Modes::StaticColor) {
			while (millis() - time_als < 2);
			static_R = Serial.read();
			static_G = Serial.read();
			static_B = Serial.read();
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
		leds[i].setRGB(static_R, static_G, static_B);
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
		leds[i] = ColorFromPalette(currentPalette, colorIndex, 255, currentBlending); // 255 - �������
		colorIndex += 1;
	}

	FastLED.delay(1000 / 60);
}
