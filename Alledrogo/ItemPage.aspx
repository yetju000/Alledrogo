<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ItemPage.aspx.cs" Inherits="ItemPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: left;
            width: 405px;
        }
        .auto-style3 {
            width: 405px;
        }
        .auto-style4 {
            text-align: right;
            width: 457px;
        }
        .auto-style5 {
            width: 457px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                            <asp:BoundField DataField="Tytuł" HeaderText="Tytuł" SortExpression="Tytuł" />
                            <asp:BoundField DataField="Opis" HeaderText="Opis" SortExpression="Opis" />
                            <asp:BoundField DataField="Typ Aukcji" HeaderText="Typ Aukcji" SortExpression="Typ Aukcji" />
                            <asp:BoundField DataField="Ilość" HeaderText="Ilość" SortExpression="Ilość" />
                            <asp:BoundField DataField="Cena za sztuke" HeaderText="Cena za sztuke" SortExpression="Cena za sztuke" />
                            <asp:BoundField DataField="Cena Aktualna" HeaderText="Cena Aktualna" SortExpression="Cena Aktualna" />
                            <asp:BoundField DataField="Sprzedawca" HeaderText="Sprzedawca" SortExpression="Sprzedawca" ReadOnly="True" />
                            <asp:BoundField DataField="Data zakończenia aukcji" HeaderText="Data zakończenia aukcji" SortExpression="Data zakończenia aukcji" />
                        </Columns>
                    </asp:GridView>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label1" runat="server" EnableTheming="True" Text="Label"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:Button ID="Button2" runat="server" Text="Akceptuj" Visible="False" OnClick="Button2_Click" />
                    <asp:Button ID="Button3" runat="server" Text="Anuluj" Visible="False" OnClick="Button3_Click" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    <div>
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegisterInput %>" SelectCommand="select I.ID, I.Title as Tytuł ,I.Description as Opis,IP.Type as 'Typ Aukcji' , IP.ItemsLeft as 'Ilość' , IP.PriceForOne as 'Cena za sztuke' , IP.ActualPrice as 'Cena Aktualna', U.Name +' '+U.surname as Sprzedawca , IP.EndDate as 'Data zakończenia aukcji'  from InProgress as IP LEFT        JOIN Items AS I ON IP.IdItem = I.Id LEFT  JOIN Users AS U ON I.IDSeller = U.ID 
  WHERE I.id LIKE '+id+'"></asp:SqlDataSource>
    </form>
</body>
</html>
