<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateReport.aspx.cs" Inherits="RPADubaiQuoteTool.GenerateReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dubai Quotation Report</title>
     <header> 
                <h1 align="center">RPA QUOTATION REPORT</h1> 
            </header> 
    
</head>
<body bgcolor="c2eaf6">
    <form id="form1" runat="server">
         <table width="50%"  align="center">
             <br /><br /><br /><br />
            <tr>
                <td>
                     <asp:Label ID="lblError" runat="server"  Font-Bold="True" ForeColor="Red" ></asp:Label>
                </td>
            </tr>
             
             <tr>
                 <td>
                     <asp:Label ID="lblCreationDate" runat="server" Text="Creation Date Interval:" Font-Bold="True"></asp:Label>
                 </td>
                 <td>

                     <asp:Calendar ID="cldCReationDateStart" runat="server" Visible="false" OnSelectionChanged="cldCReationDateStart_SelectionChanged"></asp:Calendar>
                     <asp:TextBox ID="TextBox1" runat="server" Width="128px" OnTextChanged="TextBox1_TextChanged" >
                     </asp:TextBox>
                     <asp:ImageButton ID="imgCreationDtStart" runat="server" ImageUrl="~/Calendar.png" OnClick="imgCreationDtStart_Click"  />
                     </td>
                     <td style="text-align: center; vertical-align: middle;">
                     <asp:Label ID="lbl" runat="server" Text="-"></asp:Label>
                         </td>
                     <td>
                     <asp:Calendar ID="cldCReationDateEnd" runat="server" Visible="false" OnSelectionChanged="cldCReationDateEnd_SelectionChanged"></asp:Calendar>
                     <asp:TextBox ID="TextBox2" runat="server" OnClick="TextBox2_Click" OnLoad="Page_Load" OnTextChanged="TextBox2_TextChanged" ></asp:TextBox>
                     <asp:ImageButton ID="imgCreationDtEnd" runat="server" ImageUrl="~/Calendar.png" OnClick="imgCreationDtEnd_Click"  />
                 </td>
                 
             </tr>
             <tr>
                 <td>
                     <asp:Label ID="lblStatus" runat="server" Text="Status:" Font-Bold="True"></asp:Label>
                 </td>
                 <td>
                     <asp:DropDownList ID="drpStatus" runat="server">
                         <asp:ListItem>SELECT</asp:ListItem>
                         <asp:ListItem>Draft</asp:ListItem>
                         <asp:ListItem>Pending Approval</asp:ListItem>
                         <asp:ListItem>Approved</asp:ListItem>
                         <asp:ListItem>Confirmed</asp:ListItem>
                         <asp:ListItem>Reject</asp:ListItem>
                     </asp:DropDownList>
                 </td>
             </tr>
             <tr><td></td></tr>
             <tr><td></td></tr>
             <tr>
                 <td >
                     <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" OnClick="btnGenerateReport_Click" />
                 </td>
             </tr>
         </table>
    </form>
</body>
</html>
