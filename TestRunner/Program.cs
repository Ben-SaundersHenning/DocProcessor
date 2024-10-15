using Document = DocProcessor.Document;
using DocumentType = DocProcessor.DocumentType;

string source1 = "/home/ben/Projects/DocProcessor/TestRunner/documents/NEB.docx";
string source2 = "/home/ben/Projects/DocProcessor/TestRunner/documents/Housekeeping.docx";
string savePath = "/home/ben/Projects/DocProcessor/TestRunner/documents/Merged.docx";

Document mainDoc = new Document(source1, DocumentType.ExistingDocument);
Document subDoc = new Document(source2, DocumentType.ExistingDocument);

mainDoc.ReplaceTextWithDocument("<HOUSEKEEPING>", subDoc);

subDoc.Dispose();

mainDoc.SaveAs(savePath);
mainDoc.Dispose();

