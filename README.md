# Arduino-HW-Monitor
C# app sending hardware information to arduino controller

## Visual Studio 
- [OpenHardwareMonitor lib](https://openhardwaremonitor.org/downloads/)
- [Nugget LibreHardwareMonitorLibCore 1.0.3 for OHM](https://stackoverflow.com/questions/65978314/openhardwaremonitorlib-system-io-filenotfoundexception)

## Pinout for displays

| | Waveshare 1.8 LCD | Red Ali 1.8 LCD | connects to | LOLIN D1 mini v4 | Arduino Nano |
|--|--|--|--|--|--|
| Power supply | VCC | VCC | : | 3V3 | 3V3
| Ground | GND | GND | : | GND | GND |
| Data input (Serial Data) | DIN | SDA | : | 13 MOSI | D11 |
| Clock line for SPI communication | CLK | SCK | : | 14 SCK | D13 |
| Chip Select | CS | CS | : | 15 SS | D10 |
| Data/Command control | DC | A0 | : | 0 | D7 |
| Reset | RST | RESET | : | 2 | D8 |
| Backlight control  | BL | LED | : | 4 SDA | D9 |

