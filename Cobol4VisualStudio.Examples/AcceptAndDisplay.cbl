000000 IDENTIFICATION DIVISION.
000000 PROGRAM-ID.  AcceptAndDisplay.
000000 AUTHOR.  Michael Coughlan.
000000* Uses the ACCEPT and DISPLAY verbs to accept a student record 
000000* from the user and display some of the fields.  Also shows how
000000* the ACCEPT may be used to get the system date and time.
000000* 
000000* The YYYYMMDD in "ACCEPT  CurrentDate FROM DATE YYYYMMDD." 
000000* is a format command that ensures that the date contains a 
000000* 4 digit year.  If not used, the year supplied by the system will
000000* only contain two digits which may cause a problem in the year 2000.
000000 
000000 DATA DIVISION.
000000 WORKING-STORAGE SECTION.
000000 01 StudentDetails.
000000    02  StudentId       PIC 9(7).
000000    02  StudentName.
000000        03 Surname      PIC X(8).
000000        03 Initials     PIC XX.
000000    02  CourseCode      PIC X(4).
000000    02  Gender          PIC X.
000000 
000000* YYMMDD
000000 01 CurrentDate.
000000    02  CurrentYear     PIC 9(4).
000000    02  CurrentMonth    PIC 99.
000000    02  CurrentDay      PIC 99.
000000 
000000* YYDDD
000000 01 DayOfYear.
000000    02  FILLER          PIC 9(4).
000000    02  YearDay         PIC 9(3).
000000 
000000 
000000* HHMMSSss   s = S/100
000000 01 CurrentTime.
000000    02  CurrentHour     PIC 99.
000000    02  CurrentMinute   PIC 99.
000000    02  FILLER          PIC 9(4).
000000 
000000 
000000 PROCEDURE DIVISION.
000000 Begin.
000000     DISPLAY "Enter student details using template below".
000000     DISPLAY "Enter - ID,Surname,Initials,CourseCode,Gender"
000000     DISPLAY "SSSSSSSNNNNNNNNIICCCCG".
000000     ACCEPT  StudentDetails.
000000     ACCEPT  CurrentDate FROM DATE YYYYMMDD.
000000     ACCEPT  DayOfYear FROM DAY YYYYDDD.
000000     ACCEPT  CurrentTime FROM TIME.
000000     DISPLAY "Name is ", Initials SPACE Surname.
000000     DISPLAY "Date is " CurrentDay SPACE CurrentMonth SPACE CurrentYear.
000000     DISPLAY "Today is day " YearDay " of the year".
000000     DISPLAY "The time is " CurrentHour ":" CurrentMinute.
000000     STOP RUN.