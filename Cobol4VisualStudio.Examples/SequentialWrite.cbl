000000 IDENTIFICATION DIVISION.
000000 PROGRAM-ID.  SeqWrite.
000000 AUTHOR.  Michael Coughlan.
000000* Example program showing how to create a sequential file
000000* using the ACCEPT and the WRITE verbs.
000000* Note: In this version of COBOL pressing the Carriage Return (CR)
000000* without entering any data results in StudentDetails being filled
000000* with spaces.
000000 
000000 
000000 ENVIRONMENT DIVISION.
000000 INPUT-OUTPUT SECTION.
000000 FILE-CONTROL.
000000     SELECT StudentFile ASSIGN TO "STUDENTS.DAT"
000000 		ORGANIZATION IS LINE SEQUENTIAL.
000000 
000000 DATA DIVISION.
000000 FILE SECTION.
000000 FD StudentFile.
000000 01 StudentDetails.
000000    02  StudentId       PIC 9(7).
000000    02  StudentName.
000000        03 Surname      PIC X(8).
000000        03 Initials     PIC XX.
000000    02  DateOfBirth.
000000        03 YOBirth      PIC 9(4).
000000        03 MOBirth      PIC 9(2).
000000        03 DOBirth      PIC 9(2).
000000    02  CourseCode      PIC X(4).
000000    02  Gender          PIC X.
000000 
000000 PROCEDURE DIVISION.
000000 Begin.
000000     OPEN OUTPUT StudentFile
000000     DISPLAY "Enter student details using template below.  Enter no data to end."
000000 
000000     PERFORM GetStudentDetails
000000     PERFORM UNTIL StudentDetails = SPACES
000000        WRITE StudentDetails
000000        PERFORM GetStudentDetails
000000     END-PERFORM
000000     CLOSE StudentFile
000000     STOP RUN.
000000 
000000 GetStudentDetails.
000000     DISPLAY "Enter - StudId, Surname, Initials, YOB, MOB, DOB, Course, Gender"
000000     DISPLAY "NNNNNNNSSSSSSSSIIYYYYMMDDCCCCG"
000000     ACCEPT  StudentDetails.  
000000