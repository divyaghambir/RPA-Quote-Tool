<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateNewQuote.aspx.cs"  Inherits="RPAUKQuoteTool.CreateNewQuote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<script runat="server">

   
</script>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

  
    <title>

    </title>
    <header> 
                <h1 align="center">RPA QUOTATION TOOL</h1> 
            </header> 
   
    </head>

<body bgcolor="c2eaf6">
    <form id="form1" runat="server">
 
        <table align="center" width ="90%">
            <br />
           
            <tr>
                <td><asp:Label id="lblQuoteNum" Text="Quotation Number" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtQuoteNum" Text="" runat="server" Enabled="false"  style="width: 200px"  /></td>
                <td width="20%"><asp:Label id="lblCustomer" Text="Customer" runat="server"/></td>
                <td width="20%"><asp:DropDownList id="drpCustomer" AutoPostBack="True" runat="server" style="width: 200px"  OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged">
                  <asp:ListItem Selected="True" Value="White">Select</asp:ListItem>
                  <asp:ListItem Value="Silver">DUTCO TENNANT LLC</asp:ListItem>
                  <asp:ListItem Value="DarkGray"> Customer Name </asp:ListItem>
                  <asp:ListItem Value="Khaki"> Sales Engineer </asp:ListItem>
               </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFdrpCustomer" runat="server" ControlToValidate="drpCustomer" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Submit" Display="Dynamic" InitialValue="0" EnableClientScript="true"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpCustomer" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Save" Display="Dynamic" InitialValue="0" EnableClientScript="true"></asp:RequiredFieldValidator>

                </td>
                <td width="20%"><asp:Label id="lblCustNo" Text="Customer Number" runat="server"/></td>
               
                <td>
                <asp:DropDownList id="DrpCustNo" AutoPostBack="True" runat="server" style="width: 200px"  OnSelectedIndexChanged="DrpCustNo_SelectedIndexChanged">
                  <asp:ListItem Selected="True" Value="0"> Select </asp:ListItem>
                 
               </asp:DropDownList>
                              </td>

                    <td width="20%"><asp:Label id="lblCustName" Text="Customer Name" runat="server" Visible="false"/></td>
                <td width="20%"><asp:Textbox id="txtCustName" AutoPostBack="True" runat="server" Visible="false" style="width: 200px" TextMode="MultiLine" >
                 </asp:Textbox></td>
                  </tr>
            <tr>
                 <td><asp:Label id="lblCustBranch" Text="Customer Branch" runat="server" Enabled="false"/></td>
                <td ><asp:DropDownList id="drpCustBranch" runat="server" style="width: 200px" AutoPostBack="true" OnSelectedIndexChanged="drpCustBranch_SelectedIndexChanged"  >
                    <asp:ListItem Value="0" Text="Select" />
                    <asp:ListItem Value="1" Text="SUNDRY BRANCH" />
                    </asp:DropDownList></td>   
                 <td><asp:Label id="lblCustEmail" Text="Customer Email" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustEmail" Text="" runat="server" style="width: 200px"  /></td>
                 <td><asp:Label id="lblCustPhone" Text="Customer Phone" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustPhone" Text="" runat="server" style="width: 200px"  /></td>
                 <td><asp:Label id="lblSundryBranch" Text="Sundry Branch" runat="server" Visible="false"/></td>
                <td ><asp:TextBox id="txtSundryBranch" TextMode="MultiLine" Text="" runat="server" style="width: 200px" Visible="false"  /></td>
                
            </tr>
                <tr>
                   <td width="20%"><asp:Label id="lblCarriageCharges" Text="Carriage Charges" runat="server"/></td>                
                    <td width="20%"><asp:DropDownList id="drpCarriageCharges" style="width: 200px"  AutoPostBack="True" runat="server" OnSelectedIndexChanged="drpCarriageCharges_SelectedIndexChanged">
                  <asp:ListItem Selected="True" Value="0"> Select </asp:ListItem>
                  
               </asp:DropDownList>
                        <asp:TextBox ID="txtCarriage" runat="server" Width="60px" TextMode="Number" OnTextChanged="txtCarriage_TextChanged" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFdrpCarriage" runat="server" ControlToValidate="drpCarriageCharges" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Submit" Display="Dynamic" InitialValue="0" EnableClientScript="true"></asp:RequiredFieldValidator>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="drpCarriageCharges" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Save" Display="Dynamic" InitialValue="0" EnableClientScript="true"></asp:RequiredFieldValidator>

                    </td>
                <td width="20%"><asp:Label id="lblProjectName" Text="Project Name" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtProjectName" Text="" runat="server" style="width: 200px"  />
                     <asp:RequiredFieldValidator ID="RFtxtProjectName" runat="server" ControlToValidate="txtProjectName" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProjectName" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td>
                    <td width="20%"><asp:Label id="lblOppurtunityId" Text="Oppurtunity Id" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtOppurtunityId" Text="" runat="server" style="width: 200px" />
                     <asp:RequiredFieldValidator ID="RFtxtOppurtunityId" runat="server" ControlToValidate="txtOppurtunityId" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtOppurtunityId" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td>
                </tr>
                <tr>                
               
                    
                   
                    <td width="20%"><asp:Label id="lblCurrency" Text="Currency" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtCurrency" Text="" Enabled ="false" runat="server" style="width: 200px" />
                    <asp:RequiredFieldValidator ID="RFtxtCurrency" runat="server" ControlToValidate="txtCurrency" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCurrency" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td>
                </tr>
            
                <tr>
                
                <td width="20%"><asp:Label id="lblPreparedBy" Text="Prepared By" runat="server"/></td>                
                    <td width="20%"><asp:DropDownList id="drpPreparedBy" style="width: 200px"  AutoPostBack="True" runat="server" OnSelectedIndexChanged="drpPreparedBy_SelectedIndexChanged">
                  <asp:ListItem Selected="True" Value="0"> Select </asp:ListItem>
                  
                                   
               </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFdrpPreparedBy" runat="server" ControlToValidate="drpPreparedBy" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Submit" Display="Dynamic" InitialValue="0" EnableClientScript="true"></asp:RequiredFieldValidator>
                         <asp:RequiredFieldValidator ID="RFSavedrpPreparedBy" runat="server" ControlToValidate="drpPreparedBy" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Save" Display="Dynamic" InitialValue="0" EnableClientScript="true"></asp:RequiredFieldValidator>
                    </td>
                    <td width="20%"><asp:Label id="lblSEEmail" Text="Email" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSEEMail" style="width: 200px"  Text="" runat="server" Enabled="false" /></td>
                     <td width="20%"><asp:Label id="lblSEPhone" Text="Phone" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSEPhone" Text="" runat="server" style="width: 200px" Enabled="false" /></td>
              
                </tr>
             <tr>
                
                <td width="20%"><asp:Label id="lblSalesPerson" Text="SalesPerson" style="width: 200px"  AutoPostBack="True" runat="server"> </asp:Label>  </td>
                  <td width="20%"><asp:TextBox id="txtSalesPerson" style="width: 200px"  Text="" runat="server"  /></td>
                 
              
              
                    <td width="20%"><asp:Label id="lblSPEmail" Text="Email" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSPEmail" style="width: 200px"  Text="" runat="server" /></td>
                     <td width="20%"><asp:Label id="lblSPPhone" Text="Phone" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSPPhone" Text="" runat="server" style="width: 200px" /></td>
              
                </tr>

           <tr>
                <td width="20%"><asp:Label id="lblCreationDate" Text="Creation Date" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtCreationdate" style="width: 200px"  Text="" runat="server" Enabled="false" /></td>
                     <td width="20%"><asp:Label id="lblExpirationDate" Text="Expiration Date" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtExpirationDate" Text="" runat="server" style="width: 200px" Enabled="false" /></td>
                <td ><asp:CheckBox runat="server" ID="chkPerforma" Text="Creating Proforma Invoice" Width="200px" AutoPostBack="true" OnCheckedChanged="chkPerforma_CheckedChanged" /></td>
               <td></td>
           </tr>
           <tr>
                <td width="50%"><asp:Label id="lblPerforma" Font-Bold="true" Text="" runat="server" ForeColor ="Blue"/></td>
                </tr>
              <tr>
                  <td width="50%"><asp:CheckBox ID="chkExportCustomer" runat="server" Width="200px" AutoPostBack="true" Text="Export Customer" OnCheckedChanged="chkExportCustomer_CheckedChanged" /></td>
                <td width="25%"><asp:Label id="lblVAT" Text="VAT No" runat="server" Visible="false" Width="100px" />
                   <asp:TextBox id="txtVAT" Font-Bold="true"  runat="server" Visible="false"/></td>
                </tr>
            <tr><td><asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Size="Larger"></asp:Label></td></tr>
        </table>
        <br /><br /><br /><br />
        <div id="divNewItem">
            <table  align="center" width="70%"><tr>              
                  
                    <td width="20%">
                    <asp:Button runat="server" Text="Add New Item" ID ="btnAddNewItem" OnClick="btnAddNewItem_Click" ValidationGroup="AddNewItem"/> </td>
                  </tr>
               <tr>
                <td width="50%"><asp:Label id="lblMessage" Text="" runat="server" ForeColor ="Red"/></td>
                </tr>
                
                                               </tr>

            </table>
        </div>
        <br />
        <div align="center" style="overflow-y: scroll;height: 250px; width: 100%;"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
              <Services>
                  <asp:ServiceReference Path="~/AutoCompleteTextBox.asmx" InlineScript="true" />
              </Services>
            </asp:ScriptManager>
            
            <asp:GridView ID="GridView1" width ="90%" runat="server" CssClass="Grid" AutoGenerateColumns="false" BackColor ="SkyBlue" EmptyDataText="No records has been added." OnRowDeleting="GridView1_RowDeleting"   >

    <Columns>
      
        <asp:TemplateField HeaderText="S.No.">
        <ItemTemplate>
        <asp:Label ID="lblSNo" runat="server" Text="<%# (Container.DataItemIndex)+1 %>"> 
        </asp:Label> 
        </ItemTemplate>
        </asp:TemplateField>

       <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
        <asp:BoundField DataField="Product Family" HeaderText="Product Family" ItemStyle-Width="60" />
         <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" ItemStyle-Width="60" />
        
        <asp:TemplateField HeaderText="PartNo">                    
        <ItemTemplate>
            
        
            <asp:TextBox ID="txtPartNo" runat="server" Text='<%# Bind("PartNo") %>' OnTextChanged="txtPartNo_TextChanged" Width="100px" AutoPostBack="true" ></asp:TextBox>
               <cc1:AutoCompleteExtender ID="AutoComplete" 
                              runat="server"
                              TargetControlID="txtPartNo"
                              ServicePath="~/AutoCompleteTextBox.asmx"
                              ServiceMethod="GetCompletionList"
                              MinimumPrefixLength="3" 
                              CompletionInterval="100"
                              EnableCaching="false"
                              CompletionSetCount="10"
                              FirstRowSelected="false"/>
               <asp:RequiredFieldValidator ID="RFdrpPartNumber" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
               <asp:RequiredFieldValidator ID="RFdrpPartNumber1" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPartNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="AddNewItem" EnableClientScript="true"></asp:RequiredFieldValidator>
              
    </ItemTemplate>
             
   </asp:TemplateField>
        <%--<asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="120" />--%>
        <asp:TemplateField HeaderText="Description">    
        <ItemTemplate>
            
        
            <asp:TextBox ID="txtDesc" runat="server" Text='<%# Bind("Description") %>' OnTextChanged="txtDesc_TextChanged" AutoPostBack="true" ></asp:TextBox>
               <cc1:AutoCompleteExtender ID="AutoCompleteDesc" 
                              runat="server"
                              TargetControlID="txtDesc"
                              ServicePath="~/AutoCompleteDesc.asmx"
                              ServiceMethod="GetDescList"
                              MinimumPrefixLength="3" 
                              CompletionInterval="100"
                              EnableCaching="false"
                              CompletionSetCount="10"
                              FirstRowSelected="false"/>
               <asp:RequiredFieldValidator ID="RFdrpDesc" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
               <asp:RequiredFieldValidator ID="RFdrpDesc1" runat="server" ControlToValidate="txtDesc" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
              
                </ItemTemplate>
            </asp:TemplateField>
        <asp:TemplateField HeaderText="QTY">
    <ItemTemplate>
     <asp:TextBox ID="txtQTY" Text='<%# Bind("QTY") %>' AutoPostBack="true" TextMode="Number" Width="50px" runat="server" OnTextChanged="txtQTY_TextChanged" ></asp:TextBox>
       
        <asp:RequiredFieldValidator ID="RFtxtQTY" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
               <asp:RequiredFieldValidator ID="RFtxtQTY1" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQTY" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="AddNewItem" EnableClientScript="true"></asp:RequiredFieldValidator>
               
    </ItemTemplate>
   </asp:TemplateField>
        <asp:BoundField DataField="AvailableQty" HeaderText="AvailableQty" ItemStyle-Width="120" />
        <asp:BoundField DataField="StockAvailability" HeaderText="StockAvailability" ItemStyle-Width="150" />
        <asp:BoundField DataField="MOQ" HeaderText="MOQ" ItemStyle-Width="120" />  
         <asp:TemplateField HeaderText="LeadTime">
             <ItemTemplate>
         <asp:TextBox ID="txtLeadTime" Text='<%# Bind("LeadTime") %>' AutoPostBack="true" Width="50px" TextMode="Number" runat="server" ></asp:TextBox>
           
               
        </ItemTemplate>
             </asp:TemplateField>
        
         <asp:BoundField DataField="SafetyStock" HeaderText="SafetyStock" ItemStyle-Width="120" />
         <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Width="120" />
         <asp:BoundField DataField="ListPrice" HeaderText="ListPrice" ItemStyle-Width="120" />
         <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-Width="120" />
         <asp:TemplateField HeaderText="Unit Price">
             <ItemTemplate>
         <asp:TextBox ID="txtUnitPrice" Text='<%# Eval("Unit Price") %>' AutoPostBack="true"  Width="50px"  runat="server" OnTextChanged="txtUnitPrice_TextChanged" ></asp:TextBox>
           
               
        </ItemTemplate>
             </asp:TemplateField>
        <asp:TemplateField HeaderText="AdditionalDiscount">
        <ItemTemplate>
        <asp:TextBox ID="txtDiscount" Text='<%#Eval("AdditionalDiscount") %>'  AutoPostBack="true" runat="server" Width="50px" OnTextChanged="txtDiscount_TextChanged"></asp:TextBox>
        </ItemTemplate>
       </asp:TemplateField>
        <asp:BoundField DataField="Unit Price after Extra Discount" HeaderText="Unit Price after Extra Discount" ItemStyle-Width="120" />
        <asp:BoundField DataField="Total Price after Extra Discount" HeaderText="Total Price after Extra Discount" ItemStyle-Width="120" />
        <asp:BoundField DataField="GM" HeaderText="GM" ItemStyle-Width="120" Visible="true" />
         <asp:BoundField DataField="CostPrice" HeaderText="CostPrice" ItemStyle-Width="120" Visible="true" />
       
    </Columns>

</asp:GridView>
            
    </div> 
        <br /><br />
        <table id="tblTotal" width="95%"><tr width="95%">
            <td width="20%" align="right"><asp:Label id="LblGrandTotal" Text="Grand Total" runat="server" Enabled="false"/>
                <asp:TextBox id="TxtGrandTotal" Text="0" runat="server" Enabled="false"  style="width: 100px"  />
                 <asp:Label id="lblTotalGM" Text="Total GM" runat="server" Enabled="false"/>
                <asp:TextBox id="txtTotalGM" Text="0" runat="server" Enabled="false"   />
            </td></tr>
                </table>
        <br /><br />
        <table width="95%"><tr>            
            <td width="20%" align="right">
                <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Height ="100px" Width="300px"></asp:TextBox>
                <asp:Button runat="server" Text="Save Quote" ID ="btnSave" ValidationGroup="Save" OnClick="btnSave_Click"/> 
                    <asp:Button runat="server" Text="Submit Quote" ID ="btnSubmit" ValidationGroup="Submit"  OnClick="btnSubmit_Click"/> 
                <asp:FileUpload ID="FileUpload1" runat="server" />
                
                 
                
                </td>
               </tr></table>
    </form>
</body>
</html>

