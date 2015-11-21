# EnKrypt
Custom Cipher library that I am just building to play with different ways of manipulating data.

## The Cipher

### Section 1: Alphabet

#### Step 1. Create the "alphabet" array to use.

The only rulw for creating an alphabet is it must contain all letters of the test in the array. For eaxmple "Test String" would be broke down to "Test Sring". However, you can add more characters to the alphabet to make it harder to crack the cipher if you wish.

#### Step 2. Sort Array. 

Sort the character array  by descending. Group the lower case letters by themselves, upper case letters by themselves, and then the puncuation. So the array from above "Test Sring" would become "tsrnigeTS ". 

[0] 't'
[1] 's'
[2] 'r'
[3] 'n'
[4] 'i'
[5] 'g'
[6] 'e'
[7] 'T'
[8] 'S'
[9] ' '

### Section 2. Encrypting the text

#### * Phase 1: Text to Integer

#### Step 1. Break the string into a character array. 

 [0] 'T'
 [1] 'e'
 [2] 's'
 [3] 't'
 [4] ' '
 [5] 'S'
 [6] 't'
 [7] 'r'
 [8] 'i'
 [9] 'n'
[10] 'g'

#### Step 2. Convert string to numbers.

This process is done by iterating through the string and converting the string to integers using the following formula. 

(alphabet length) to the power of (the letter array's length - 1 for each iteration through). Multiply that number by (the letter's index) plus 1.

Note that the max value of an integer is 2,147,483,647. Since we can not convert a larger string into integers this way, we must convert the string into multiple integers.

String 1: "Test Stri" (872,209,135)
String 2: "ng" (46)

String 1:
Starting from index [0] 'T':
(10 ^ (9 - 1)) * (7 + 1) = 800,000,000

Then [1] 'e':
(10 ^ (9 - 2)) * (6 + 1) = 70,000,000

Then [2] 's':
(10 ^ (9 - 3)) * (1 + 1) = 2,000,000

Then [3] 't':
(10 ^ (9 - 4)) * (0 + 1) = 100,000

Then [4] ' ':
(10 ^ (9 - 5)) * (9 + 1) = 100,000

Then [5] 'S':
(10 ^ (9 - 6)) * (8 + 1) = 9,000

Then [6] 't':
(10 ^ (9 - 7)) * (0 + 1) = 100

Then [7] 'r':
(10 ^ (9 - 8)) * (2 + 1) = 30

Then [8] 'i':
(10 ^ (9 - 9)) * (4 + 1) = 5

You then add them all together = 872,209,135

String 2:
Starting from index [0] 'n':
(10 ^ (2 - 1)) * (3 + 1) = 40

Starting from index [1] 'g':
(10 ^ (2 - 2)) * (5 + 1) = 6

You then add them all together = 46


#### * Phase 2: Integer to Duotrigesimal (ConvertBase10ToBase32)

This process uses a vary similar formula as phase 1 does. During this phase we take an integer (base 10) and beak it up into smaller chunks that can be represented as duotrigesimals (base 32). Duotrigesimal is similar to Hexidecimal (base 16) in the sense that it uses letters to represent numbers over 9. Duotrigesimal does the same thing, it just has more letters. This allows us to store much bigger numbers with fewer characters. For example, if we take the 2 integers from Phase 1 and convert them to Duotrigesimal, we will get R8 6H 47 1E. "R86H471E" is more space convenient than "872209135.46" (I am just using a '.' to seperate the integer value so they can me stored in a string).

To be documented...
