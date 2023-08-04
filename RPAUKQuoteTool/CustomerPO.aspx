<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerPO.aspx.cs" Inherits="RPAUKQuoteTool.CustomerPO" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UK Quotation</title>
     <header> 
                <h1 align="center">RPA QUOTATION REPORT</h1> 
            </header> 
    
    
</head>
<body bgcolor="c2eaf6">
    
    <form id="form1" runat="server">
         <table width="50%"  align="center">
            
            <tr>
                <td>
                     <asp:Label ID="lblError" runat="server"  Font-Bold="True" ForeColor="Red" ></asp:Label>
                </td>
            </tr>
             <br />
             <tr>
                 <td>
                     <asp:Label ID="lblQuoteNum" runat="server" Text="Quote Number" Font-Bold="True"></asp:Label>
                 </td>
                 <td>

                      <asp:TextBox ID="txtQuoteNum" runat="server" Width="128px" Enabled="false" >
                     </asp:TextBox>
                    
                 </td>
                 
             </tr>
              <br />
              <tr>
                 <td>
                     <asp:Label ID="lblStatus" runat="server" Text="Status" Font-Bold="True"></asp:Label>
                 </td>
                  <td>
                      <asp:DropDownList ID="drpStatus" runat="server" Width="135px">
                          <asp:ListItem>Please select</asp:ListItem>
                          <asp:ListItem>Accept</asp:ListItem>
                           <asp:ListItem>Reject</asp:ListItem>
                      </asp:DropDownList>
                  </td>
             </tr>
              <br />
             
             <tr>
                 <td>
                     <asp:Label ID="lblCustomerPO" runat="server" Text="Customer PO#" Font-Bold="True"></asp:Label>
                </td>
                 <td>
                     <asp:TextBox ID="txtCustomerPO" runat="server" Width="128px" Enabled="true" >
                     </asp:TextBox>
                 </td>
             </tr>
              <br />
            
             <tr>
                 <td>
                                      
                     </td>
                 <td>
                     <asp:FileUpload ID="FileUpload1"  runat="server" Width="200px" />
                     <asp:Label ID="lblUpload" ForeColor="Blue" runat="server" Text="(Please upload your PO document here in PDF format)"></asp:Label>
                 </td>
             </tr>
              <br />
             <br />
             <br />
             <tr>
                 <td></td></tr>
              </table>
        <br /><br />
        <table width="95%">
             <tr>
                 <td colspan="2" align="center">
                     <asp:Button ID="btnSubmit" runat="server" Text="SAVE" Width="150px" OnClick="btnSubmit_Click" />
                 </td>
             </tr>
         </table>
    </form>
</body>
</html>
