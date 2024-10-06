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

#include <TFT_eSPI.h> // Graphics and font library for ST7735 driver chip
#include <SPI.h>

TFT_eSPI tft = TFT_eSPI();  // Invoke library, pins defined in User_Setup.h

unsigned long targetTime = 0; // Used for testing draw times

void setup(void) {
  tft.init();
  tft.setRotation(3);
}

void loop() {
  targetTime = millis();

  // First we test them with a background colour set
  //tft.setTextSize(1);
  tft.fillScreen(TFT_GREEN);
  
  // tft.drawLine(1, 0, 126, 0, TFT_BROWN);
  // tft.drawLine(0, 1, 0, 158, TFT_BROWN);
  // tft.drawLine(1, 159, 126, 159, TFT_BROWN);
  // tft.drawLine(127, 1, 127, 158, TFT_BROWN);

  tft.drawLine(0, 0, 30, 30, TFT_BROWN);
  //Ola kod
  /*
  tft.drawLine(12, 10, 12, 39, TFT_PINK);
  tft.drawLine(12, 40, 29, 40, TFT_PINK);
  tft.drawLine(36, 10, 50, 10, TFT_PINK);
  tft.drawLine(36, 40, 50, 40, TFT_PINK);
  tft.drawLine(36, 40, 50, 40, TFT_PINK);
  tft.drawLine(36, 10, 36, 40, TFT_PINK);
  tft.drawLine(50, 10, 50, 40, TFT_PINK);
  tft.drawLine(36, 10, 36, 40, TFT_PINK);
  tft.drawLine(57, 10, 66, 40, TFT_PINK);
  tft.drawLine(66, 40, 78, 10, TFT_PINK);
  tft.drawLine(85, 10, 85, 40, TFT_PINK);
  tft.drawLine(85, 10, 100, 10, TFT_PINK);
  tft.drawLine(85, 25, 100, 25, TFT_PINK);
  tft.drawLine(85, 40, 100, 40, TFT_PINK);
  */
  /// po Oli kodzie
  delay(4000);
}

