# TelescopeRC

A hardware and software project that adds remote focusing and capture triggering on a hobby grade telescope.

## Hardware

Arduino nano listening with UART for commands sent through HC-06 bluetooth module is used to control DC motor that drives focusing wheel and a relay that is connected to a DSLR external trigger port.

## Software

An android app written in Xamarin.Forms that allows connecting to HC-06 module with simple UI for sending commands to the arduino.
