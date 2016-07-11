
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace Cobol4VisualStudio.Extension {

    internal sealed class CobolSyntaxHighlighter : ITagger<CobolSyntaxTag> {

        ITextBuffer _buffer;

        private static List<CobolSyntaxDefinition> definitions = new List<CobolSyntaxDefinition>() {

            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Comment, Expression = new Regex(@"(\*.*)") },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.SequenceNumber, Expression = new Regex(@"^[\d]{6}") },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Division, Expression = new Regex(@"(?<=(IDENTIFICATION|ID|ENVIRONMENT|DATA|PROCEDURE))[\s]+DIVISION(?=(\.|[\s]??(USING|GIVING|RETURNING)))", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.DivisionName, Expression = new Regex(@"\b(IDENTIFICATION|ID|ENVIRONMENT|DATA|PROCEDURE)(?=[\s]+DIVISION(\.|[\s]??(USING|GIVING|RETURNING)))", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Section, Expression = new Regex(@"(?<=[\w\d-]{1,32}[\s]+)SECTION(?=\.)", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.SectionName, Expression = new Regex(@"\b([A-Za-z0-9-]+)(?=[\s]+SECTION\.)", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Constant, Expression = new Regex(@"\b(?<![-])(FILLER|HIGH-VALUE[S]??|LOW-VALUE[S]??NULL[S]??|QUOTE[S]??|SPACE[S]??|ZERO(ES|S)??)\b(?!-)", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.String, Expression = new Regex(@"([""']).*?(\1)") },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Keyword, Expression =  new Regex(@"\b(?<![-])(ACCEPT|ACCESS|ACTIVE-CLASS|ADD|ADDRESS|ADVANCING|AFTER|ALIGNED|ALL|ALLOCATE|ALPHABET|ALPHABETIC|ALPHABETIC-LOWER|ALPHABETIC-UPPER|ALPHANUMERIC|ALPHANUMERIC-EDITED|ALSO|ALTER|ALTERNATE|AND|ANY|ANYCASE|ARE|AREA|AREAS|ARGUMENT-NUMBER|ARGUMENT-VALUE|ARITHMETIC|AS|ASCENDING|ASSIGN|AT|ATTRIBUTE|AUTO|AUTO-SKIP|AUTOMATIC|AUTOTERMINATE|B-AND|B-NOT|B-OR|B-XOR|BACKGROUND-COLOR|BASED|BEEP|BEFORE|BELL|BINARY|BINARY-C-LONG|BINARY-CHAR|BINARY-DOUBLE|BINARY-LONG|BINARY-SHORT|BIT|BLANK|BLINK|BLOCK|BOOLEAN|BOTTOM|BY|BYTE-LENGTH|CALL|CANCEL|CD|CENTER|CF|CH|CHAIN|CHAINING|CHARACTER|CHARACTERS|CLASS|CLASS-ID|CLASSIFICATION|CLOSE|CODE|CODE-SET|COL|COLLATING|COLS|COLUMN|COLUMNS|COMMA|COMMAND-LINE|COMMIT|COMMON|COMMUNICATION(?!\s+SECTION\.)|(COMP(UTATIONAL)?)(-[1-6X])?|COMPUTE|CONDITION|CONFIGURATION(?!\s+SECTION\.)|CONSTANT|CONTAINS|CONTENT|CONTINUE|CONTROL|CONTROLS|CONVERTING|COPY|CORR|CORRESPONDING|COUNT|CRT|CURRENCY|CURSOR|CYCLE|DATA(?!\s+DIVISION\.)|DATA-POINTER|DATE|DAY|DAY-OF-WEEK|DE|DEBUGGING|DECIMAL-POINT|DECLARATIVES|DEFAULT|DELETE|DELIMITED|DELIMITER|DEPENDING|DESCENDING|DESTINATION|DETAIL|DISABLE|DISK|DISPLAY|DIVIDE|DOWN|DUPLICATES|DYNAMIC|EBCDIC|EC|EGI|ELSE|EMI|ENABLE|END|END-ACCEPT|END-ADD|END-CALL|END-COMPUTE|END-DELETE|END-DISPLAY|END-DIVIDE|END-EVALUATE|END-IF|END-MULTIPLY|END-OF-PAGE|END-PERFORM|END-READ|END-RECEIVE|END-RETURN|END-REWRITE|END-SEARCH|END-START|END-STRING|END-SUBTRACT|END-UNSTRING|END-WRITE|ENTRY|ENTRY-CONVENTION|ENVIRONMENT(?!\s+DIVISION\.)|ENVIRONMENT-NAME|ENVIRONMENT-VALUE|EO|EOL|EOP|EOS|EQUAL|EQUALS|ERASE|ERROR|ESCAPE|ESI|EVALUATE|EXCEPTION|EXCEPTION-OBJECT|EXCLUSIVE|EXIT|EXPANDS|EXTEND|EXTERNAL|FACTORY|FALSE|FD|FILE(?!\s+SECTION\.)|FILE-CONTROL|FILE-ID|FINAL|FIRST|FLOAT-BINARY-16|FLOAT-BINARY-34|FLOAT-BINARY-7|FLOAT-DECIMAL-16|FLOAT-DECIMAL-34|FLOAT-EXTENDED|FLOAT-LONG|FLOAT-SHORT|FOOTING|FOR|FOREGROUND-COLOR|FOREVER|FORMAT|FREE|FROM|FULL|FUNCTION|FUNCTION-ID|FUNCTION-POINTER|GENERATE|GET|GIVING|GLOBAL|GO|GOBACK|GREATER|GROUP|GROUP-USAGE|HEADING|HIGHLIGHT|I-O|I-O-CONTROL|ID(?!\s+DIVISION\.)|IDENTIFICATION(?!\s+DIVISION\.)|IF|IGNORE|IGNORING|IMPLEMENTS|IN|INDEX|INDEXED|INDICATE|INFINITY|INHERITS|INITIAL|INITIALIZE|INITIALIZED|INITIATE|INPUT|INPUT-OUTPUT(?!\s+SECTION\.)|INSPECT|INTERFACE|INTERFACE-ID|INTO|INTRINSIC|INVALID|INVOKE|IS|JUST|JUSTIFIED|KEY|LABEL|LAST|LC_ALL|LC_COLLATE|LC_CTYPE|LC_MESSAGES|LC_MONETARY|LC_NUMERIC|LC_TIME|LEADING|LEFT|LENGTH|LESS|LIMIT|LIMITS|LINAGE|LINAGE-COUNTER|LINE|LINE-COUNTER|LINES|LINKAGE(?!\s+SECTION\.)|LOCAL-STORAGE|LOCALE|LOCK|LOWLIGHT|MANUAL|MEMORY|MERGE|MESSAGE|METHOD|METHOD-ID|MINUS|MODE|MOVE|MULTIPLE|MULTIPLY|NATIONAL|NATIONAL-EDITED|NATIVE|NEGATIVE|NESTED|NEXT|NO|NONE|NORMAL|NOT|NUMBER|NUMBERS|NUMERIC|NUMERIC-EDITED|OBJECT|OBJECT-COMPUTER|OBJECT-REFERENCE|OCCURS|OF|OFF|OMITTED|ON|ONLY|OPEN|OPTIONAL|OPTIONS|OR|ORDER|ORGANIZATION|OTHER|OUTPUT|OVERFLOW|OVERLINE|OVERRIDE|PACKED-DECIMAL|PADDING|PAGE|PAGE-COUNTER|PARAGRAPH|PERFORM|PF|PH|PIC|PICTURE|PLUS|POINTER|POSITION|POSITIVE|PRESENT|PREVIOUS|PRINTER|PRINTING|PROCEDURE(?!\s+DIVISION\.)|PROCEDURE-POINTER|PROCEDURES|PROCEED|PROGRAM|PROGRAM-ID|PROGRAM-POINTER|PROMPT|PROPERTY|PROTOTYPE|PURGE|QUEUE|RAISE|RAISING|RANDOM|RD|READ|RECEIVE|RECORD|RECORDING|RECORDS|RECURSIVE|REDEFINES|REEL|REFERENCE|RELATION|RELATIVE|RELEASE|REMAINDER|REMOVAL|RENAMES|REPLACE|REPLACING|REPORT|REPORTING|REPORTS|REPOSITORY|REQUIRED|RESERVE|RESET|RESUME|RETRY|RETURN|RETURNING|REVERSE-VIDEO|REWIND|REWRITE|RF|RH|RIGHT|ROLLBACK|ROUNDED|RUN|SAME|SCREEN(?!\s+SECTION\.)|SCROLL|SD|SEARCH|SECONDS|SECURE|SEGMENT|SEGMENT-LIMIT|SELECT|SELF|SEND|SENTENCE|SEPARATE|SEQUENCE|SEQUENTIAL|SET|SHARING|SIGN|SIGNED|SIGNED-INT|SIGNED-LONG|SIGNED-SHORT|SIZE|SORT|SORT-MERGE|SOURCE|SOURCE-COMPUTER|SOURCES|SPECIAL-NAMES|STANDARD|STANDARD-1|STANDARD-2|START|STATEMENT|STATUS|STEP|STOP|STRING|STRONG|SUB-QUEUE-1|SUB-QUEUE-2|SUB-QUEUE-3|SUBTRACT|SUM|SUPER|SUPPRESS|SYMBOL|SYMBOLIC|SYNC|SYNCHRONIZED|SYSTEM-DEFAULT|TABLE|TALLYING|TAPE|TERMINAL|TERMINATE|TEST|TEXT|THAN|THEN|THROUGH|THRU|TIME|TIMES|TO|TOP|TRAILING|TRANSFORM|TRUE|TYPE|TYPEDEF|UCS-4|UNDERLINE|UNIT|UNIVERSAL|UNLOCK|UNSIGNED|UNSIGNED-INT|UNSIGNED-LONG|UNSIGNED-SHORT|UNSTRING|UNTIL|UP|UPDATE|UPON|USAGE|USE|USER-DEFAULT|USING|UTF-16|UTF-8|VAL-STATUS|VALID|VALIDATE|VALIDATE-STATUS|VALUE|VALUES|VARYING|WAIT|WHEN|WITH|WORDS|WORKING-STORAGE(?!\s+SECTION\.)|WRITE|YYYYDDD|YYYYMMDD)\b(?!-)", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Paragraph, Expression = new Regex(@"(?<=(^[\d]{6}\s{1,4}))(?<![-])(?=[A-Za-z0-9-]*?[A-Z])(?<![-])\b[A-Za-z0-9-]{1,30}\b(?!-)(?=\.)", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Number, Expression = new Regex(@"(?<=[ ]+?)[\d]*[.]*[\d]*(?=[ \r\n]+?)") }, 
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Identifier, Expression = new Regex(@"\b(?<![-])(?=[A-Za-z0-9-]*?[A-Z])(?<![-])\b[A-Za-z0-9-]{1,30}\b(?!-)") },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.PictureLevel, Expression = new Regex(@"(?<=(^[\d]{6}\s{1,}))[\d]{1,2}(?=\s.*)") },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Picture, Expression = new Regex(@"(picture\s+is|picture|pic\s+is|pic)\s+[ABPSVXZ(),/$-.+*0-9]*(CR|DB|CS|)", RegexOptions.IgnoreCase) },
            new CobolSyntaxDefinition() { Type = CobolSyntaxTypes.Symbol, Expression = new Regex(@"(?<=picture\s+is|picture|pic\s+is|pic\s+.*)[().]*", RegexOptions.IgnoreCase) },
            
        };




        internal CobolSyntaxHighlighter(ITextBuffer buffer) {
            _buffer = buffer;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged {
            add { }
            remove { }
        }

        public IEnumerable<ITagSpan<CobolSyntaxTag>> GetTags(NormalizedSnapshotSpanCollection spans) {

            if (spans.Count == 0 || spans[0].Length == 0 || spans[0].Length >= _buffer.CurrentSnapshot.Length) {
                yield break;
            }

            foreach (SnapshotSpan span in spans) {
                
                ITextSnapshotLine line = span.Start.GetContainingLine();
                int location = line.Start.Position;
                string text = line.GetText();

                foreach (var definition in definitions) {
                    if (definition.Expression.IsMatch(text)) {
                        MatchCollection matches = definition.Expression.Matches(text);

                        foreach (Match m in matches) {
                            yield return new TagSpan<CobolSyntaxTag>(new SnapshotSpan(span.Snapshot, location + m.Index, m.Length), new CobolSyntaxTag(definition.Type));
                        }

                    }
                }
                

            }

        }
    }
}
