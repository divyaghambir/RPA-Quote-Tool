<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterPage.aspx.cs" Inherits="RPADubaiQuoteTool.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <table align="right"> <tr><td><asp:Image id="Image1" runat="server" ImageUrl="~\images\logo.png" ></asp:Image></td></tr></table>
      <header> 
                <h1 align="center">WATTS QUOTATION TOOL</h1> 
                
            </header> 
</head>
<body  bgcolor="c2eaf6">
    <form id="form1" runat="server">
       <table width="40%" align="center">
           <br />
           <br />
           <br />
           <br />
           <tr>
               <td align="center">
                    <asp:HyperLink Id="hyp1"  runat="server" ImageUrl="~/Images/Internal.PNG" Text="Internal tool" ImageHeight="200" ImageWidth="200" NavigateUrl="~/Login.aspx"></asp:HyperLink>
               </td>
           
               <td align="center">
                     <asp:HyperLink Id="hyp2" runat="server" ImageUrl="~/Images/External.PNG" Text="External tool" ImageHeight="200" ImageWidth="200" NavigateUrl="~/Cust_Login.aspx"></asp:HyperLink>
               </td>
           </tr>
           <tr>
               <td align="center">
                   <asp:Label ID="lblInternal" runat="server" Text="For Watts Employees UK" Font-Bold="true" ForeColor="Blue"></asp:Label>
               </td>
           
               <td align="center">
                     <asp:Label ID="lblExternal" runat="server" Text="For External users" Font-Bold="true" ForeColor="Blue"></asp:Label>
               </td>
           </tr>
       </table>
       
       

    </form>
</body>
</html>
