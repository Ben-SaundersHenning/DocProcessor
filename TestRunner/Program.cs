using Document = DocProcessor.Document;
using DocumentType = DocProcessor.DocumentType;

string neb = "/home/ben/Projects/DocProcessor/TestRunner/documents/NEB.docx";
string housekeeping = "/home/ben/Projects/DocProcessor/TestRunner/documents/Housekeeping.docx";
string activities = "/home/ben/Projects/DocProcessor/TestRunner/documents/Activities.docx";
string home_environment = "/home/ben/Projects/DocProcessor/TestRunner/documents/Home_Environment.docx";
string physical = "/home/ben/Projects/DocProcessor/TestRunner/documents/Physical.docx";
string reason_for_referral = "/home/ben/Projects/DocProcessor/TestRunner/documents/Reason_For_Referral.docx";
string savePath = "/home/ben/Projects/DocProcessor/TestRunner/documents/Merged.docx";

Document mainDoc = new Document(neb, DocumentType.ExistingDocument);

Document doc1 = new Document(housekeeping, DocumentType.ExistingDocument);
Document doc2 = new Document(activities, DocumentType.ExistingDocument);
Document doc3 = new Document(home_environment, DocumentType.ExistingDocument);
Document doc4 = new Document(physical, DocumentType.ExistingDocument);
Document doc5 = new Document(reason_for_referral, DocumentType.ExistingDocument);

mainDoc.ReplaceTextWithDocument("<HOUSEKEEPING>", doc1);
mainDoc.ReplaceTextWithDocument("<ACTIVITIES>", doc2);
mainDoc.ReplaceTextWithDocument("<HOME_ENVIRONMENT>", doc3);
mainDoc.ReplaceTextWithDocument("<PHYSICAL>", doc4);
mainDoc.ReplaceTextWithDocument("<REASON_FOR_REFERRAL>", doc5);

doc1.Dispose();
doc2.Dispose();
doc3.Dispose();
doc4.Dispose();
doc5.Dispose();

mainDoc.SaveAs(savePath);
mainDoc.Dispose();

