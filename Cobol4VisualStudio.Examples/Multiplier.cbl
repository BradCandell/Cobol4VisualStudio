000000 IDENTIFICATION DIVISION.
000000 PROGRAM-ID.  Multiplier.
000000 AUTHOR.  Michael Coughlan.
000000* Example program using ACCEPT, DISPLAY and MULTIPLY to 
000000* get two single digit numbers from the user and multiply them together
000000 
000000 DATA DIVISION.
000000 
000000 WORKING-STORAGE SECTION.
000000 01  Num1                                PIC 9  VALUE ZEROS.
000000 01  Num2                                PIC 9  VALUE ZEROS.
000000 01  Result                              PIC 99 VALUE ZEROS.
000000 
000000 PROCEDURE DIVISION.
000000     DISPLAY "Enter first number  (1 digit) : " WITH NO ADVANCING.
000000     ACCEPT Num1.
000000     DISPLAY "Enter second number (1 digit) : " WITH NO ADVANCING.
000000     ACCEPT Num2.
000000     MULTIPLY Num1 BY Num2 GIVING Result.
000000     DISPLAY "Result is = ", Result.
000000     STOP RUN.
000000 
000000 
