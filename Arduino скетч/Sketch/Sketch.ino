#include <FastLED.h>

enum Modes{
  Static,
  Ambilight,
  PolishFlag,
  OFF,
  NotInstalled = 113
};

byte mode = Modes::NotInstalled;
byte r = 0, g = 0, b = 0;
unsigned long passed_time = 0;
bool connected_to_pc = false;
bool LED_OFF = true;
const byte LED_LENGTH = 46;
CRGB leds[LED_LENGTH];

void setup() {
  FastLED.addLeds<NEOPIXEL, 6>(leds, LED_LENGTH); // 6 - пин, подающий сигнал; 46 - количество светодиодов
  Serial.begin(9600);
  connected_to_pc = false;
  byte mode = Modes::NotInstalled;
  pinMode(2, OUTPUT); // питание реле
  pinMode(3, OUTPUT); // приемник сигнала реле
  digitalWrite(2, HIGH);
  digitalWrite(3, LOW); // по умолчанию реле закрыто
}

void loop() {
  if(!connected_to_pc){
    Serial.write("LED Strip\n");
  }
  else if(mode == Modes::Static){
    for(int i = 0; i < LED_LENGTH; ++i) leds[i].setRGB(r, g, b);
    FastLED.show();
  }
  else if(mode == Modes::PolishFlag){
    for(int i = 4 ; i <= 26 ; ++i) leds[i].setRGB(255, 255, 255);
    for(int i = 27 ; i <= 45 ; ++i) leds[i].setRGB(255, 0, 0);
    for(int i = 0 ; i <= 3 ; ++i) leds[i].setRGB(255, 0, 0);
    FastLED.show();
  }
  delay(100);
}

byte count = 0; // Необходим для записи rgb. Если == 3, то обнуляем
int leds_count = 0; // Необходим для записи данных, для эмбилайт режима. Если >= LED_LENGTH, то обнуляем

void serialEvent() {
  while(Serial.available()){ 
    if(millis() - passed_time > 1000) {
      mode = Modes::NotInstalled;
      count = 0;
      leds_count = 0;
    }
    if(mode == Modes::NotInstalled) mode = Serial.read();
    if(mode == Modes::OFF){
      connected_to_pc = false;
      digitalWrite(3, LOW);
      LED_OFF = true;
    }
    else if (mode == Modes::Static){
      if(count == 0) r = Serial.read();
      else if(count == 1) g = Serial.read();
      else if(count == 2) b = Serial.read();
      if(++count >= 3) count = 0;
    }
    else if(mode == Modes::Ambilight){
      if(leds_count >= LED_LENGTH) leds_count = 0;
      if(Serial.available() > 3){
        leds[leds_count].r = Serial.read();
        leds[leds_count].g = Serial.read();
        leds[leds_count++].b = Serial.read();
      }
      FastLED.show();
    }
    if((mode == Modes::Static || mode == Modes::Ambilight || mode == Modes::PolishFlag) && (LED_OFF || !connected_to_pc)){
      if(!connected_to_pc) connected_to_pc = true;
      if(LED_OFF){
        digitalWrite(3, HIGH);
        LED_OFF = false;
      }
    }
    
    passed_time = millis();
  }
}
