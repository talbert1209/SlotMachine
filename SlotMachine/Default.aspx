<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SlotMachine.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: large;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial, Helvetica, sans-serif">
            <h1>Lady Luck Slots</h1>
            <br />
            <asp:Image ID="image1" runat="server" Height="200px" Width="200px" />
&nbsp;<asp:Image ID="image2" runat="server" Height="200px" Width="200px" />
&nbsp;<asp:Image ID="image3" runat="server" Height="200px" Width="200px" />
            <br />
            <br />
            Your Bet: <asp:TextBox ID="betTextBox" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="pullLeverButton" runat="server" Text="Pull The Lever!" />
            <br />
            <br />
            <asp:Label ID="resultLabel" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Label ID="moneyLabel" runat="server"></asp:Label>
            <br />
            <br />
            <strong><span class="auto-style1">Rules:</span></strong><br />
            1. Cherry - x2 Your Bet<br />
            2. Cherries - x3 Your Bet<br />
            3. Cherries - x4 Your Bet<br />
            4. 7&#39;s - Jackpot - x100 Your Bet<br />
            HOWEVER... if there&#39;s even one BAR you win nothing</div>
    </form>
</body>
</html>
