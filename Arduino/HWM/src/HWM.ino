
// Pause in milliseconds between screens, change to 0 to time font rendering
#define WAIT 500

#include <TFT_eSPI.h>  // Graphics and font library for ST7735 driver chip
//#include <SPI.h>
#include "FPSCounter.h"
#include "ScreenModelData.h"
#include "SerialController.h"
// #include "Orbitron_Medium_10.h"
// #include "Orbitron_Medium_16.h"
// #include "Orbitron_Medium_15.h"
// #include "Font4x5Fixed.h"
// #include "Font4x7Fixed.h"
// #include "Font5x5Fixed.h"
// #include "Font5x7Fixed.h"
// #include "Font5x7FixedMono.h"

//#include "DisplayedValue.h"

TFT_eSPI tft = TFT_eSPI(); // Invoke library, pins defined in User_Setup.h
FPSCounter fps = FPSCounter();
SerialController serialController = SerialController();  

unsigned long millis_time = 0;
//received com messages
int received = 0;
bool screenModeChanged = true;
char screenBrightness = '2';

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
  setBrightness(screenBrightness);
  serialController.init();
  drawHeader();
}

void loop() {
  millis_time = millis();
  serialController.readData();
  fps.countFps();
  drawDebugInfo();
}

void executeCommand(String commandData) {
  Serial.print("Processing command: ");
  Serial.println(commandData);
  char commandChar = commandData.charAt(0);
  //char value = command.charAt(3);

  switch (commandChar) {
    case 'B':
      {
        setBrightness('2');
        break;
      }
    case 'M':
      {
        setScreenMode(commandData.substring(2));
        break;
      }
  }
}

void drawDebugInfo() {
  if (!fps.fpsValue.valueChanged())
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
  for (int i = 0; i < 9; i++) {
    tft.setTextColor(TFT_GREEN, TFT_BLACK);
    tft.drawString("TH1", 0, 16 * i + 12);

    endIndex = data.indexOf(';', startIndex + 1);
    String singleData = data.substring(startIndex, endIndex);
    startIndex = endIndex + 1;

    tft.setTextColor(TFT_GREEN, TFT_BLACK);
    if (singleData.toDouble() > 50) {
      tft.setTextColor(TFT_YELLOW, TFT_BLACK);
    }
    //tft.drawString(singleData, 2, 14*i + 12, 2);

    tft.drawString(singleData, 34, 14 * i + 12);
  }
}

void drawHeader() {
  tft.fillRect(0, 0, 159, 12, TFT_ORANGE);
  tft.drawLine(80, 13, 80, 128, TFT_WHITE);
  tft.drawLine(81, 13, 81, 128, TFT_WHITE);
  //tft.fillRect(80, 13, 81, 120, TFT_WHITE);
  tft.setTextColor(TFT_WHITE);
  tft.drawString("CPU LOAD", 5, -2, 2);
}

void setScreenMode(String commandData) {

  Serial.print("Processing screen command: ");
  Serial.println(commandData);

  // char screenMode = '0';
  // String screenLabel = "Wait";
  // String dataUnit = "na";
  // int dataSize = 0;

  int startIndex = 0;
  int endIndex;
  for (int i = 0; i < 4; i++) {
    //tft.setTextColor(TFT_GREEN, TFT_BLACK);
    //tft.drawString("TH1", 0, 16 * i + 12);

    //C>B:V=1
    //C>M:V=1:L=CPU TEMP:U=%:S=8

    endIndex = commandData.indexOf(':', startIndex + 1);
    String singleData = commandData.substring(startIndex, endIndex);
    startIndex = endIndex + 1;

    int indexOfSplitter = singleData.indexOf('=');
    String type = singleData.substring(0, indexOfSplitter);
    String value = singleData.substring(indexOfSplitter+1);
  }
}

void setBrightness(char brightness) {
  screenBrightness = brightness;

  switch (brightness) {
    case '0':
      {
        analogWrite(PIN_D2, 0);
        //analogWrite(9, 0); ???
        break;
      }
    case '1':
      {
        analogWrite(PIN_D2, 20);
        break;
      }
    case '2':
      {
        analogWrite(PIN_D2, 255);
        break;
      }
  }
}
