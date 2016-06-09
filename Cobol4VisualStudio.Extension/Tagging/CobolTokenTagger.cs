﻿
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {


    internal sealed class CobolTokenTagger : ITagger<CobolTokenTag> {

        ITextBuffer _buffer;

        private static IDictionary<CobolTokenTypes, List<Regex>> tokenParsers = new Dictionary<CobolTokenTypes, List<Regex>>() {
            { CobolTokenTypes.Comment, new List<Regex>() { new Regex(@"(\*>.*$|(?<=[\d]{6})\*.*)") } },
            { CobolTokenTypes.Keyword, new List<Regex>() { new Regex(@"\b(?<![-])(ACCEPT|ACCESS|ACTIVE-CLASS|ADD|ADDRESS|ADVANCING|AFTER|ALIGNED|ALL|ALLOCATE|ALPHABET|ALPHABETIC|ALPHABETIC-LOWER|ALPHABETIC-UPPER|ALPHANUMERIC|ALPHANUMERIC-EDITED|ALSO|ALTER|ALTERNATE|AND|ANY|ANYCASE|ARE|AREA|AREAS|ARGUMENT-NUMBER|ARGUMENT-VALUE|ARITHMETIC|AS|ASCENDING|ASSIGN|AT|ATTRIBUTE|AUTO|AUTO-SKIP|AUTOMATIC|AUTOTERMINATE|B-AND|B-NOT|B-OR|B-XOR|BACKGROUND-COLOR|BASED|BEEP|BEFORE|BELL|BINARY|BINARY-C-LONG|BINARY-CHAR|BINARY-DOUBLE|BINARY-LONG|BINARY-SHORT|BIT|BLANK|BLINK|BLOCK|BOOLEAN|BOTTOM|BY|BYTE-LENGTH|CALL|CANCEL|CD|CENTER|CF|CH|CHAIN|CHAINING|CHARACTER|CHARACTERS|CLASS|CLASS-ID|CLASSIFICATION|CLOSE|CODE|CODE-SET|COL|COLLATING|COLS|COLUMN|COLUMNS|COMMA|COMMAND-LINE|COMMIT|COMMON|COMMUNICATION|COMP|COMP-1|COMP-2|COMP-3|COMP-4|COMP-5|COMP-X|COMPUTATIONAL|COMPUTATIONAL-1|COMPUTATIONAL-2|COMPUTATIONAL-3|COMPUTATIONAL-4|COMPUTATIONAL-5|COMPUTATIONAL-X|COMPUTE|CONDITION|CONFIGURATION|CONSTANT|CONTAINS|CONTENT|CONTINUE|CONTROL|CONTROLS|CONVERTING|COPY|CORR|CORRESPONDING|COUNT|CRT|CURRENCY|CURSOR|CYCLE|DATA|DATA-POINTER|DATE|DAY|DAY-OF-WEEK|DE|DEBUGGING|DECIMAL-POINT|DECLARATIVES|DEFAULT|DELETE|DELIMITED|DELIMITER|DEPENDING|DESCENDING|DESTINATION|DETAIL|DISABLE|DISK|DISPLAY|DIVIDE|DIVISION|DOWN|DUPLICATES|DYNAMIC|EBCDIC|EC|EGI|ELSE|EMI|ENABLE|END|END-ACCEPT|END-ADD|END-CALL|END-COMPUTE|END-DELETE|END-DISPLAY|END-DIVIDE|END-EVALUATE|END-IF|END-MULTIPLY|END-OF-PAGE|END-PERFORM|END-READ|END-RECEIVE|END-RETURN|END-REWRITE|END-SEARCH|END-START|END-STRING|END-SUBTRACT|END-UNSTRING|END-WRITE|ENTRY|ENTRY-CONVENTION|ENVIRONMENT|ENVIRONMENT-NAME|ENVIRONMENT-VALUE|EO|EOL|EOP|EOS|EQUAL|EQUALS|ERASE|ERROR|ESCAPE|ESI|EVALUATE|EXCEPTION|EXCEPTION-OBJECT|EXCLUSIVE|EXIT|EXPANDS|EXTEND|EXTERNAL|FACTORY|FALSE|FD|FILE|FILE-CONTROL|FILE-ID|FILLER|FINAL|FIRST|FLOAT-BINARY-16|FLOAT-BINARY-34|FLOAT-BINARY-7|FLOAT-DECIMAL-16|FLOAT-DECIMAL-34|FLOAT-EXTENDED|FLOAT-LONG|FLOAT-SHORT|FOOTING|FOR|FOREGROUND-COLOR|FOREVER|FORMAT|FREE|FROM|FULL|FUNCTION|FUNCTION-ID|FUNCTION-POINTER|GENERATE|GET|GIVING|GLOBAL|GO|GOBACK|GREATER|GROUP|GROUP-USAGE|HEADING|HIGH-VALUE|HIGH-VALUES|HIGHLIGHT|I-O|I-O-CONTROL|ID|IDENTIFICATION|IF|IGNORE|IGNORING|IMPLEMENTS|IN|INDEX|INDEXED|INDICATE|INFINITY|INHERITS|INITIAL|INITIALIZE|INITIALIZED|INITIATE|INPUT|INPUT-OUTPUT|INSPECT|INTERFACE|INTERFACE-ID|INTO|INTRINSIC|INVALID|INVOKE|IS|JUST|JUSTIFIED|KEY|LABEL|LAST|LC_ALL|LC_COLLATE|LC_CTYPE|LC_MESSAGES|LC_MONETARY|LC_NUMERIC|LC_TIME|LEADING|LEFT|LENGTH|LESS|LIMIT|LIMITS|LINAGE|LINAGE-COUNTER|LINE|LINE-COUNTER|LINES|LINKAGE|LOCAL-STORAGE|LOCALE|LOCK|LOW-VALUE|LOW-VALUES|LOWLIGHT|MANUAL|MEMORY|MERGE|MESSAGE|METHOD|METHOD-ID|MINUS|MODE|MOVE|MULTIPLE|MULTIPLY|NATIONAL|NATIONAL-EDITED|NATIVE|NEGATIVE|NESTED|NEXT|NO|NONE|NORMAL|NOT|NULL|NULLS|NUMBER|NUMBERS|NUMERIC|NUMERIC-EDITED|OBJECT|OBJECT-COMPUTER|OBJECT-REFERENCE|OCCURS|OF|OFF|OMITTED|ON|ONLY|OPEN|OPTIONAL|OPTIONS|OR|ORDER|ORGANIZATION|OTHER|OUTPUT|OVERFLOW|OVERLINE|OVERRIDE|PACKED-DECIMAL|PADDING|PAGE|PAGE-COUNTER|PARAGRAPH|PERFORM|PF|PH|PIC|PICTURE|PLUS|POINTER|POSITION|POSITIVE|PRESENT|PREVIOUS|PRINTER|PRINTING|PROCEDURE|PROCEDURE-POINTER|PROCEDURES|PROCEED|PROGRAM|PROGRAM-ID|PROGRAM-POINTER|PROMPT|PROPERTY|PROTOTYPE|PURGE|QUEUE|QUOTE|QUOTES|RAISE|RAISING|RANDOM|RD|READ|RECEIVE|RECORD|RECORDING|RECORDS|RECURSIVE|REDEFINES|REEL|REFERENCE|RELATION|RELATIVE|RELEASE|REMAINDER|REMOVAL|RENAMES|REPLACE|REPLACING|REPORT|REPORTING|REPORTS|REPOSITORY|REQUIRED|RESERVE|RESET|RESUME|RETRY|RETURN|RETURNING|REVERSE-VIDEO|REWIND|REWRITE|RF|RH|RIGHT|ROLLBACK|ROUNDED|RUN|SAME|SCREEN|SCROLL|SD|SEARCH|SECONDS|SECTION|SECURE|SEGMENT|SEGMENT-LIMIT|SELECT|SELF|SEND|SENTENCE|SEPARATE|SEQUENCE|SEQUENTIAL|SET|SHARING|SIGN|SIGNED|SIGNED-INT|SIGNED-LONG|SIGNED-SHORT|SIZE|SORT|SORT-MERGE|SOURCE|SOURCE-COMPUTER|SOURCES|SPACE|SPACES|SPECIAL-NAMES|STANDARD|STANDARD-1|STANDARD-2|START|STATEMENT|STATUS|STEP|STOP|STRING|STRONG|SUB-QUEUE-1|SUB-QUEUE-2|SUB-QUEUE-3|SUBTRACT|SUM|SUPER|SUPPRESS|SYMBOL|SYMBOLIC|SYNC|SYNCHRONIZED|SYSTEM-DEFAULT|TABLE|TALLYING|TAPE|TERMINAL|TERMINATE|TEST|TEXT|THAN|THEN|THROUGH|THRU|TIME|TIMES|TO|TOP|TRAILING|TRANSFORM|TRUE|TYPE|TYPEDEF|UCS-4|UNDERLINE|UNIT|UNIVERSAL|UNLOCK|UNSIGNED|UNSIGNED-INT|UNSIGNED-LONG|UNSIGNED-SHORT|UNSTRING|UNTIL|UP|UPDATE|UPON|USAGE|USE|USER-DEFAULT|USING|UTF-16|UTF-8|VAL-STATUS|VALID|VALIDATE|VALIDATE-STATUS|VALUE|VALUES|VARYING|WAIT|WHEN|WITH|WORDS|WORKING-STORAGE|WRITE|YYYYDDD|YYYYMMDD|ZERO|ZEROES|ZEROS)\b(?!-)", RegexOptions.IgnoreCase) } },
            { CobolTokenTypes.Division, new List<Regex>() { new Regex(@"\b(IDENTIFICATION|ID|ENVIRONMENT|DATA|PROCEDURE)[\s]+DIVISION\.", RegexOptions.IgnoreCase) } },
            { CobolTokenTypes.Section, new List<Regex>() { new Regex(@"(^|[\t\s])\b(?<![-])[\w\d-]+[\w\d][\s]+SECTION\.", RegexOptions.IgnoreCase) } },
            { CobolTokenTypes.String, new List<Regex>() { new Regex(@"([""']).*?(\1)") } },
            { CobolTokenTypes.LineNumber, new List<Regex>() { new Regex(@"^[\d]{6}") } }
        };



        public CobolTokenTagger(ITextBuffer buffer) {
            _buffer = buffer;
        }


        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }

        /// <summary>
        /// Brad is testing
        /// </summary>
        /// <param name="spans"></param>
        /// <returns></returns>
        public IEnumerable<ITagSpan<CobolTokenTag>> GetTags(NormalizedSnapshotSpanCollection spans) {
            
            if (spans.Count == 0 || spans[0].Length == 0 || spans[0].Length >= _buffer.CurrentSnapshot.Length) {
                yield break;
            }

            foreach (SnapshotSpan span in spans) {


                ITextSnapshotLine containingLine = span.Start.GetContainingLine();
                int currentLocation = containingLine.Start.Position;

                string text = containingLine.GetText();

                foreach (var key in tokenParsers.Keys) {
                    foreach(Regex r in tokenParsers[key]) {
                        if (r.IsMatch(text)) {
                            Match m = r.Match(text);

                            //if (key == CobolTokenTypes.Division) {
                            //    currentDivions = m.Value;
                            //}

                            //System.Diagnostics.Debug.WriteLine("{0} -- {1}: {2}", new object[] { key.ToString(), m.Value, currentDivions });
                            yield return new TagSpan<CobolTokenTag>(new SnapshotSpan(span.Snapshot, currentLocation + m.Index, m.Length), new CobolTokenTag(key));

                        }
                    }
                }

            }

            yield break;

        }
    }

}