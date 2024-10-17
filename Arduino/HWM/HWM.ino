/*
 Display all the fast rendering fonts.

 This sketch uses the GLCD (font 1) and fonts 2, 4, 6, 7, 8
 
 Make sure all the display driver and pin connections are correct by
 editing the User_Setup.h file in the TFT_eSPI library folder.

 #########################################################################
 ###### DON'T FORGET TO UPDATE THE User_Setup.h FILE IN THE LIBRARY ######
 #########################################################################
*/

// Pause in milliseconds between screens, change to 0 to time font rendering
#define WAIT 500

#include <TFT_eSPI.h>  // Graphics and font library for ST7735 driver chip
#include <SPI.h>
#include "FPSCounter.h"
#include "Orbitron_Medium_10.h"
#include "Orbitron_Medium_16.h"
#include "Orbitron_Medium_15.h"
#include "Font4x5Fixed.h"
#include "Font4x7Fixed.h"
#include "Font5x5Fixed.h"
#include "Font5x7Fixed.h"
#include "Font5x7FixedMono.h"

//#include "DisplayedValue.h"

TFT_eSPI tft = TFT_eSPI();
FPSCounter fps = FPSCounter();  // Invoke library, pins defined in User_Setup.h

unsigned long millis_time = 0;
//received com messages
int received = 0;
char screenMode = '0';
bool screenModeChanged = true;
char screenBrightness = '1';

#define MYFONT10 &Orbitron_Medium_10
#define MYFONT15 &Orbitron_Medium_15
#define MYFONT16 &Orbitron_Medium_16

#define DEFAU &Font7srle



#define MYFONT1 &Font4x5Fixed
#define MYFONT2 &Font4x7Fixed
#define MYFONT3 &Font5x5Fixed
#define MYFONT4 &Font5x7Fixed
#define MYFONT5 &Font5x7FixedMono

void setup(void) {
  tft.init();
  tft.setRotation(1);
  tft.fillScreen(TFT_BLACK);
  Serial.begin(9600);
  Serial.setTimeout(10);
  setBrightness(screenBrightness);
  drawHeader();
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
      //Serial.println("Received data...");
      drawCPU(str.substring(2));
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
  if(!fps.fpsValue.valueChanged())
    return;

  tft.setTextColor(TFT_PURPLE, TFT_ORANGE);
  tft.fillRect(90, 2, 160, 9, TFT_ORANGE);
  tft.setTextFont(1);
  tft.setTextSize(1);
  String fpsInfo = String("FPS:") += fps.fpsValue.getValue();
  tft.drawString(fpsInfo, 90, 2);
}

void drawCPU(String data) {
  tft.setTextColor(TFT_RED, TFT_BLACK);
  tft.setTextFont(2);
  tft.setTextSize(1);

  int startIndex = 0;
  int endIndex;
  for(int i = 0; i < 9; i++)
  {
    tft.setTextColor(TFT_GREEN, TFT_BLACK);
    tft.drawString("TH1", 0, 16*i + 12);

    endIndex = data.indexOf(';', startIndex + 1);
    String singleData = data.substring(startIndex, endIndex);
    startIndex = endIndex + 1;
    
    tft.setTextColor(TFT_GREEN, TFT_BLACK);
    if(singleData.toDouble() > 50) {
      tft.setTextColor(TFT_YELLOW, TFT_BLACK);
    }
    //tft.drawString(singleData, 2, 14*i + 12, 2);
    
    tft.drawString(singleData, 34, 14*i + 12);
  }
}

void drawHeader() {
  tft.fillRect(0, 0, 159, 12, TFT_ORANGE);
  tft.drawLine(80,13,80,128, TFT_WHITE);
  tft.drawLine(81,13,81,128, TFT_WHITE);
  //tft.fillRect(80, 13, 81, 120, TFT_WHITE);
  tft.setTextColor(TFT_WHITE);
  tft.drawString("CPU LOAD", 5, -2, 2);
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
      //analogWrite(9, 0); ???
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

