/*
 Display all the fast rendering fonts.

 This sketch uses the GLCD (font 1) and fonts 2, 4, 6, 7, 8
 
 Make sure all the display driver and pin connections are correct by
 editing the User_Setup.h file in the TFT_eSPI library folder.

 #########################################################################
 ###### DON'T FORGET TO UPDATE THE User_Setup.h FILE IN THE LIBRARY ######
 #########################################################################
*/

// New background colour
#define TFT_BROWN 0x38E0

// Pause in milliseconds between screens, change to 0 to time font rendering
#define WAIT 500

#include <TFT_eSPI.h>  // Graphics and font library for ST7735 driver chip
#include <SPI.h>
#include "FPSCounter.h"
//#include "DisplayedValue.h"

TFT_eSPI tft = TFT_eSPI();
FPSCounter fps = FPSCounter();  // Invoke library, pins defined in User_Setup.h

unsigned long millis_time = 0;
//received com messages
int received = 0;
char screenMode = '0';
bool screenModeChanged = true;
char screenBrightness = '1'; 

void setup(void) {
  tft.init();
  tft.setRotation(1);
  tft.fillScreen(TFT_BLACK);
  Serial.begin(9600);
  Serial.setTimeout(10);
  setBrightness(screenBrightness);
}

void loop() {
  millis_time = millis();

  if (Serial.available() > 0) {
    String str = Serial.readString();
    str.trim();
    Serial.print("Received message: ");
    Serial.println(str);
    received++;

    if(str.charAt(0) == 'C') {
      executeCommand(str.substring(2));
    } else if(str.charAt(0) == 'D') {
      Serial.println("Received data...");
    }
  }

  fps.countFps();
  drawDebugInfo();
}



void executeCommand(String command) {
  Serial.print("Processing command: ");
  Serial.println(command);
  char commandChar = command.charAt(0);
  char value = command.charAt(2);

  switch(commandChar) {
    case 'B': {
      setBrightness(value);
      break;
    }
    case 'S': {
      setScreenMode(value);
      break;
    }
  }
}

void drawDebugInfo() {
  // tft.setTextColor(TFT_BLACK, TFT_RED);
  // String info = String("Mode: ") += String(screenMode);
  // tft.drawString(info, 3, 0, 2);

  // String brightnessInfo = String("Bright: ") += String(screenBrightness);
  // tft.drawString(brightnessInfo, 65, 0, 2);

  if(fps.fpsValue.valueChanged())
  {
    String fpsInfo = String("FPS: ") += fps.fpsValue.getValue();
    tft.drawString(fpsInfo, 3, 20, 2);
  }
 
}

void setScreenMode(char mode) {
  screenModeChanged = true;
  screenMode = mode;
  
    // tft.fillRect(0, 0, 159, 32, TFT_BLACK);
    // tft.setTextColor(TFT_WHITE, TFT_BLACK);
    // tft.drawString(String(received), 0, 55, 4);
    // tft.drawString(String(sizeof(str)), 0, 80, 4);
    // tft.setTextColor(TFT_GREEN, TFT_BLACK);
    // tft.drawString(str, 0, 10, 4);
    // tft.drawString(str, 0, 10, 4);
}

void setBrightness(char brightness) {
  screenBrightness = brightness;

  switch(brightness) {
    case '0': {
      analogWrite(PIN_D2, 0);
      break;
    }
    case '1': {
      analogWrite(PIN_D2, 20);
      break;
    }
    case '2': {
      analogWrite(PIN_D2, 255);
      break;
    }
  }
  
}

