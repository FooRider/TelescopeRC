#define BUFFER_SIZE 200
#define MOTOR_AIN1 9
#define MOTOR_AIN2 10
#define MOTOR_APWM 11
#define MOTOR_STBY 12

char inputBuffer[BUFFER_SIZE];
int inputBufferWriteIdx = 0;

int appliedFocusingCommandNumber = 0;
int expiredFocusingCommandNumber = 0;
int currentFocusingCommandNumber = 0;
int currentFocusingCommandSpeed = 0;
int currentFocusingCommandExpiration = 0;

void setup() {
  Serial.begin(9600);

  pinMode(MOTOR_STBY, OUTPUT);
  pinMode(MOTOR_AIN1, OUTPUT);
  pinMode(MOTOR_AIN2, OUTPUT);
  pinMode(MOTOR_APRM, OUTPUT);
}

void loop() {
  // check focusing state
  if (appliedFocusingCommandNumber != currentFocusingCommandNumber) { // new command appeared
    setFocusingMotorSpeed(currentFocusingCommandSpeed);
    appliedFocusingCommandNumber = currentFocusingCommandNumber;
  }
  if (appliedFocusingCommandNumber != expiredFocusingCommandNumber
      && appliedFocusingCommandNumber == currentFocusingCommandNumber
      && millis() >= currentFocusingCommandExpiration) { // current command expired
    setFocusingMotorSpeed(0);
    expiredFocusingCommandNumber = appliedFocusingCommandNumber;
  }
}

void setFocusingMotorSpeed(byte speed)
{

}

void onFocusingCommand(int speed, int duration)
{
  currentFocusingCommandSpeed = speed;
  currentFocusingCommandExpiration = millis() + duration;
  currentFocusingCommandNumber++;
}

void serialEvent() {
  while (Serial.available()) {
    char inChar = (char)Serial.read();
    inputBuffer[inputBufferWriteIdx] = inChar;
    inputBufferWriteIdx++;

    if (inChar == '\n')
    {
      tryExecuteCommand();
      inputBufferWriteIdx = 0;
    }

    if (inputBufferWriteIdx >= BUFFER_SIZE)
    {
      inputBufferWriteIdx = 0;
    }
  }
}

void tryExecuteCommand()
{
  if (inputBufferWriteIdx < 4) return;
  if (inputBuffer[inputBufferWriteIdx - 1] != '\n') return;
  if (inputBuffer[inputBufferWriteIdx - 2] != '\r') return;

  int readIdx = 0;

  if (inputBuffer[0] == 'F' && inputBuffer[1] == ' ') { // Command "F +<speed> <duration>\r\n"
    readIdx += 2;
    int direction = 0;    
    if (inputBuffer[readIdx] == '+') direction = 1;
    if (inputBuffer[readIdx] == '-') direction = -1;
    if (direction == 0) return;
    readIdx++;

    int speed;
    int numberSize = parseIntFromBuffer(readIdx, inputBufferWriteIdx - readIdx, &speed);
    if (numberSize <= 0) return;
    readIdx += numberSize;

    if (readIdx >= inputBufferWriteIdx) return;
    if (inputBuffer[readIdx] != ' ') return;
    readIdx++;

    if (readIdx >= inputBufferWriteIdx) return;
    int duration;
    numberSize = parseIntFromBuffer(readIdx, inputBufferWriteIdx - readIdx, &duration);
    if (numberSize <= 0) return;
    readIdx += numberSize;

    if (readIdx >= inputBufferWriteIdx) return;
    if (inputBuffer[readIdx++] != '\r') return;
    if (readIdx >= inputBufferWriteIdx) return;
    if (inputBuffer[readIdx++] != '\n') return;

    onFocusingCommand(speed, duration);
    return;
  }
  // else if () {} // other commands
}

int parseIntFromBuffer(int offset, int maxLength, int *value) {
  int position = 0;
  *value = 0;
  while (position < maxLength && inputBuffer[offset + position] >= '0' && inputBuffer[offset + position] <= '9')
  {
    *value = *value * 10 + (inputBuffer[offset + position] - '0');
    position++;
  }
  return position;
}