using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace RPADubaiQuoteTool
{
    public class ITextEvents : PdfPageEventHelper
    {
         // This is the contentbyte object of the writer  
   PdfContentByte cb;  
  
    // we will put the final number of pages in a template  
    PdfTemplate headerTemplate, footerTemplate;  
  
    // this is the BaseFont we are going to use for the header / footer  
    BaseFont bf = null;  
  
    // This keeps track of the creation time  
    DateTime PrintTime = DateTime.Now;      
 
    #region Fields  
    private string _header;  
    #endregion  
 
    #region Properties  
    public string Header  
    {  
        get { return _header; }  
        set { _header = value; }  
    }  
    #endregion      
  
    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
            PrintTime = DateTime.Now;
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            headerTemplate = cb.CreateTemplate(100, 100);
            footerTemplate = cb.CreateTemplate(50, 50);



        }
        private iTextSharp.text.Font font = FontFactory.GetFont("Times Roman", 12, iTextSharp.text.Font.TIMES_ROMAN);

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
    {  
        base.OnEndPage(writer, document);      
        iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(font);      
        iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(font);      
        Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);  
  
      
  
       
        String text = "Page " + writer.PageNumber + " of ";

            /*PrintTime = DateTime.Now;
            bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb = writer.DirectContent;
            headerTemplate = cb.CreateTemplate(100, 100);
            footerTemplate = cb.CreateTemplate(50, 50);*/


            //Add paging to footer  
            {  
                 
                cb.BeginText();  
            cb.SetFontAndSize(bf, 12);  
            cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(30));  
            cb.ShowText(text);  
            cb.EndText();  
            float len = bf.GetWidthPoint(text, 12);  
            cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));  
        }  
  
     
  
  
        //Move the pointer and draw line to separate footer section from rest of page  
        /*cb.MoveTo(40, document.PageSize.GetBottom(50) );  
        cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));  
        cb.Stroke();  */
    }  
  
    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
            base.OnCloseDocument(writer, document);
            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();

        }



    }
}