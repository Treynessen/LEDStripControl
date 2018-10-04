#include <FastLED.h>

#define VERTICAL_LEDS_NUM 8 // количество светодиодов на 1 вертикальной полосе
#define HORIZONTAL_LEDS_NUM 15 // количество светодиодов на 1 горизонтальной полосе
#define NUM_LEDS (2 * VERTICAL_LEDS_NUM + 2 * HORIZONTAL_LEDS_NUM) // общее количество светодиодов
#define LED_CONTROL_PIN 6 // управляющий пин (с которого подается сигнал на ленту)
#define RELAY_POWER 2 // пин, питающий реле
#define RELAY_SIGNAL 3 // пин, управляющий реле

enum Modes{
  StaticColor,
  Animation,
  Ambilight,
  PolishFlag,
  NotConnected
};

byte static_color[3] = {0, 0, 0}; // цвет для StaticColor режима

byte mode = Modes::NotConnected;

unsigned long time_als = 0; // time after last signal

// анимация
CRGBPalette16 currentPalette = RainbowColors_p;
TBlendType currentBlending = LINEARBLEND;
byte startIndex = 0;

CRGB leds[NUM_LEDS]; // лента

void setup() {
  Serial.begin(1000000);
  FastLED.addLeds<NEOPIXEL, LED_CONTROL_PIN>(leds, (2 * VERTICAL_LEDS_NUM + 2 * HORIZONTAL_LEDS_NUM));
  pinMode(RELAY_POWER, OUTPUT);
  digitalWrite(RELAY_POWER, HIGH);
  pinMode(RELAY_SIGNAL, OUTPUT);
  RelayOn(false);
}

void loop() {
  if(mode == Modes::NotConnected) Serial.write("LED Strip\n");
  
  
}

void RelayOn(bool on){
  if(on) digitalWrite(RELAY_SIGNAL, HIGH);
  else digitalWrite(RELAY_SIGNAL, LOW);
}

void StaticColorMode(){
  for(int i = 0; i < NUM_LEDS; ++i){
    leds[i].setRGB(static_color[0], static_color[1], static_color[2]);
  }
  FastLED.show();
}

void PolishFlagMode(){
  for(int i=0; i < VERTICAL_LEDS_NUM / 2; ++i){
    leds[i].setRGB(255, 0, 0);
    leds[2 * VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM -1 - i].setRGB(255, 0, 0);
  }
  for(int i = VERTICAL_LEDS_NUM / 2; i < VERTICAL_LEDS_NUM; ++i){
    leds[i].setRGB(255, 255, 255);
    leds[2 * VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM -1 - i].setRGB(255, 255, 255);
  }
  for(int i = VERTICAL_LEDS_NUM; i < VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM; ++i){
    leds[i].setRGB(255, 255, 255);
    leds[VERTICAL_LEDS_NUM + HORIZONTAL_LEDS_NUM + i].setRGB(255, 0, 0);
  }
  FastLED.show();
}

void AnimationMode(){
  ++startIndex;

  byte colorIndex = startIndex;
  for (int i = 0; i < NUM_LEDS; ++i) {
    leds[i] = ColorFromPalette(currentPalette, colorIndex, 255, currentBlending); // 255 - яркость
    colorIndex += 1;
  }

  FastLED.delay(1000 / 60);
}
