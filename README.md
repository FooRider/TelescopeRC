# TelescopeRC

A hardware and software project that adds remote focusing and capture triggering on a hobby grade telescope.

## Hardware

Arduino nano listening with UART for commands sent through HC-06 bluetooth module is used to control DC motor that drives focusing wheel and a relay that is connected to a DSLR external trigger port.

## Software

An android app written in Xamarin.Forms that allows connecting to HC-06 module with simple UI for sending commands to the arduino.

# Comm protocol

Each command for the arduino consists of ASCII chars ending with CR and LF characters.

## Focusing

Focusing in this project means controlling DC motor attached to telescope focus mechanism. The aim is to provide means to control
the motor finely.

Each focusing command has two parameters. First parameter is desired motor speed on a range of -255 to +255. The sign is mandatory and is used to indicate direction of movement. Second parameter is duration in milliseconds that the speed is to be maintained.

The arduino can execute only one focusing command at a time, therefore each received command overrides previous command.

```
F +140 1500
```

## Capturing
