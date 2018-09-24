#include <FastLED.h>

enum Mods{
  Static,
  Dynamic,
  OFF,
  NotInstalled = 113,
  NotConnected
};

byte mod = 113;
byte r = 0, g = 0, b = 0;
unsigned long passed_time = 0;
bool connected_to_pc = false;
bool LED_OFF = true;
const byte LED_LENGTH = 46;
CRGB leds[46];

void setup() {
  FastLED.addLeds<NEOPIXEL, 6>(leds, 46); // 6 - пин, подающий сигнал; 46 - количество светодиодов
  Serial.begin(9600);
  connected_to_pc = false;
  pinMode(2, OUTPUT); // питание реле
  pinMode(3, OUTPUT); // приемник сигнала реле
  digitalWrite(2, HIGH);
  digitalWrite(3, LOW); // по умолчанию реле закрыто
  
}

void loop() {
  if(!connected_to_pc){
    Serial.write("LED Strip\n");
  }
  else if(mod == 0){
    for(int i = 0; i < LED_LENGTH; ++i){
      leds[i].setRGB(r, g, b);
    }
    FastLED.show();
  }
  else if(mod == 1){
    
  }
  delay(100);
}

byte count = 0; // Необходим для записи rgb. Если == 3, то обнуляем

void serialEvent() {
  while(Serial.available()){ 
    if(!connected_to_pc) connected_to_pc = true;
    if(millis() - passed_time > 1000) mod = Mods::NotInstalled;
    if(mod == Mods::NotInstalled) mod = Serial.read();
    else if (mod == Mods::Static){
      if(LED_OFF){
        digitalWrite(3, HIGH);
        LED_OFF = false;
      }
      if(count == 0) r = Serial.read();
      else if(count == 1) g = Serial.read();
      else if(count == 2) b = Serial.read();
      if(++count == 3) count = 0;
    }
    passed_time = millis();
  }
}
