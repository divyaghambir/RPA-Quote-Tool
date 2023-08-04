<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cust_CreateNewQuote.aspx.cs" Inherits="RPAUKCustomerQuote.Cust_CreateNewQuote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <header> 
                <h1 align="center">RPA CUSTOMER QUOTATION</h1> 
         <style>
        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
  
        input[type=number] {
            -moz-appearance: textfield;
        }
    </style>
            </header> 
</head>
<body bgcolor="c2eaf6">
     <form id="form1" runat="server">
 
        <table align="center" width ="90%">
            <br />
           
            <tr>
                <td><asp:Label id="lblQuoteNum" Text="Quotation Number" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtQuoteNum" Text="" runat="server" Enabled="false"  style="width: 200px"  /></td>
                <td width="20%"><asp:Label id="lblCustName" Text="Customer Name" runat="server" /></td>
                <td width="20%"><asp:Textbox id="txtCustName" runat="server" style="width: 200px" TextMode="MultiLine" >
                 </asp:Textbox>

                </td>
                <td width="20%"><asp:Label id="lblCustNo" Text="Customer Number" runat="server"/></td>
               
                <td>
                <asp:TextBox id="txtCustNo" runat="server" style="width: 200px">
                                  
               </asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFCUstNo" runat="server" ControlToValidate="txtCustNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RFCUstNo1" runat="server" ControlToValidate="txtCustNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>

                              </td>

                  </tr>
            <tr>
                 <td><asp:Label id="lblCustEmail" Text="Customer Email" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustEmail" Text="" runat="server" style="width: 200px"  /></td>   
                 <td><asp:Label id="lblCustPhone" Text="Customer Phone" runat="server" Enabled="false"/></td>
                <td ><asp:TextBox id="txtCustPhone" Text="" runat="server" style="width: 200px"  /></td>
                <td width="20%"><asp:Label id="lblCurrency" Text="Currency" runat="server"/></td>
                <td width="20%"><asp:DropDownList id="drpCurrency" runat="server" style="width: 200px" >
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem>EUR</asp:ListItem>
                    <asp:ListItem>GBP</asp:ListItem>
                      </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFtxtCurrency" runat="server" ControlToValidate="drpCurrency" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="drpCurrency" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td>
                
            </tr>
                <tr>
                   <td width="20%"><asp:Label id="lblCarriageCharges" Text="Carriage Charges" runat="server"/></td>                
                    <td width="20%">
                        <asp:TextBox ID="txtCarriage" runat="server" Width="60px" TextMode="Number" OnTextChanged="txtCarriage_TextChanged" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFdrpCarriage" runat="server" ControlToValidate="txtCarriage" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Submit" Display="Dynamic" EnableClientScript="true"></asp:RequiredFieldValidator>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtCarriage" ForeColor="Red"
                        ErrorMessage="Required Field." ValidationGroup="Save" Display="Dynamic" EnableClientScript="true"></asp:RequiredFieldValidator>

                    </td>
                <td width="20%"><asp:Label id="lblProjectName" Text="Project Name" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtProjectName" Text="" runat="server" style="width: 200px"  />
                     <asp:RequiredFieldValidator ID="RFtxtProjectName" runat="server" ControlToValidate="txtProjectName" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtProjectName" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td>
                    <td width="20%"><asp:Label id="lblRef" Text="Reference No" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtRefNo" Text="" runat="server" style="width: 200px" />
                     <asp:RequiredFieldValidator ID="RFtxtOppurtunityId" runat="server" ControlToValidate="txtRefNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Submit" EnableClientScript="true"></asp:RequiredFieldValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRefNo" ForeColor="Red" Display="Dynamic"
                        ErrorMessage="Required Field." ValidationGroup="Save" EnableClientScript="true"></asp:RequiredFieldValidator>
                </td>
                </tr>
               
            
                <tr>
                
                <td width="20%"><asp:Label id="lblPreparedBy" Text="Prepared By" runat="server"/></td>                
                    <td width="20%"><asp:DropDownList id="drpPreparedBy" style="width: 200px" runat="server" OnSelectedIndexChanged="drpPreparedBy_SelectedIndexChanged">
                  <asp:ListItem Selected="True" Value="0"> Select </asp:ListItem>
                  
                                   
               </asp:DropDownList>
                      
                    </td>
                    <td width="20%"><asp:Label id="lblSEEmail" Text="Email" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSEEMail" style="width: 200px"  Text="" runat="server" Enabled="false" /></td>
                     <td width="20%"><asp:Label id="lblSEPhone" Text="Phone" runat="server"/></td>
                <td width="20%"><asp:TextBox id="txtSEPhone" Text="" runat="server" style="width: 200px" Enabled="false" /></td>
              
                </tr>
             <tr>
                
                <td width="20%"><asp:Label id="lblSalesPerson" Text="SalesPerson" style="width: 200px"  runat="server"> </asp:Label>  </td>
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
                <td >&nbsp;</td>
               <td></td>
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
                
             

            </table>
        </div>
        <br />
        <div align="center" style="overflow-y: scroll;height: 250px; width: 100%;"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
              <Services>
                  <asp:ServiceReference Path="~/AutoCompleteTextBox.asmx" InlineScript="false" />
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
         <asp:BoundField DataField="LeadTime" HeaderText="LeadTime" ItemStyle-Width="120" />  
         <asp:BoundField DataField="SafetyStock" HeaderText="SafetyStock" ItemStyle-Width="120" />
         <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Width="120" />
         <asp:BoundField DataField="ListPrice" HeaderText="ListPrice" ItemStyle-Width="120" />
         <asp:BoundField DataField="Discount" HeaderText="Discount" ItemStyle-Width="120" />
         <asp:BoundField DataField="CostPrice" HeaderText="CostPrice" ItemStyle-Width="120" />
        <asp:BoundField DataField="Total" HeaderText="Total" ItemStyle-Width="120" />
        
       
       
    </Columns>

</asp:GridView>
            
    </div> 
        <br /><br />
        <table id="tblTotal" width="95%"><tr width="95%">
            <td width="20%" align="right"><asp:Label id="LblGrandTotal" Text="Grand Total" runat="server" Enabled="false"/>
                <asp:TextBox id="TxtGrandTotal" Text="0" runat="server" Enabled="false"  style="width: 100px"  />
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
