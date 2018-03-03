<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="set_a.aspx.cs" Inherits="stu_set_a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <%--<link rel="stylesheet" href="Content/StyleSheet.css" type="text/css"/>--%>
    <%--<link rel="stylesheet" href="Content/moco.min.css" type="text/css"/>--%>
    <div style=" font-size: 14px;
    height: 58px;
    line-height: 58px;
    margin-top: 10px;
    border-bottom: 1px solid #d0d6d9;
    border-bottom-width: 1px;
    border-bottom-style: solid;
    border-bottom-color: rgb(208, 214, 217);">
      
       <div style="float: left;"> 
        <span style="color: red;
                border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)"><a href="set.aspx">个人信息</a></span>
        </div> 
         <div style="float: left;margin-left:20px;">
         <span style="color: red;
                  border-bottom: 2px solid #f01400;
                border-bottom-width: 2px;
                border-bottom-style: solid;
                border-bottom-color: rgb(240, 20, 0)"><a href="set_a.aspx">安全设置</a></span>
             </div>
        <div style="float: right;">
            <%--<div style="position: relative;margin-right: 10px;">
                <span class="cursor: pointer"><a href="set_edit">编辑</a></span>
            </div>--%>

        </div>
    </div>
    <div style=" padding: 0 0 0 64px;
                 padding-top: 5px;
                 padding-right: 0px;
                 padding-bottom: 0px;
                 padding-left: 64px;">
        <div style="position: relative;
                    padding-left: 37px;
                    border-left: 1px solid #d3d7da;
                    border-left-width: 1px;
                    border-left-style: solid;
                    border-left-color: rgb(211, 215, 218);">

    <span style="position: absolute;left: 0;top: 20px;margin-left: -64px;line-height: 16px;font-size: 12px;color: #8a8c8f;">
               
                <b style="font-size: 18px;font-weight: 400;">
                    <asp:Label ID="YEAR" runat="server" ></asp:Label></b>
                <em style="display: block;margin-top: 2px;">
                    <asp:Label ID="DAY" runat="server" Text=""></asp:Label></em>
            </span>

          <%--  <asp:DataList ID="DataList1" runat="server">
            <ItemTemplate>--%>
     <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 绑定手机号:
                    <asp:TextBox ID="phone" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px" Text =""></asp:TextBox>
             <span style=" font-size: 15px;color:red;">（*必填）</span>
                </h3>
            </div>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 绑定邮箱:
                    <asp:TextBox ID="email" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px" Text=""></asp:TextBox>
                    <span style=" font-size: 15px;color:red;">（*必填）</span>
                </h3>
            </div>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 旧密码:
                    <asp:TextBox ID="oldword" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px" Text=""></asp:TextBox>
             <span style=" font-size: 15px;color:red;">（*区分大小写）</span>
                </h3>
            </div>
               <%-- </ItemTemplate>
                </asp:DataList>--%>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;">新密码:
                    <asp:TextBox ID="newword" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px" ></asp:TextBox>
                </h3>
            </div>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;">确认密码:
                    <asp:TextBox ID="quereng" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
             <span style=" font-size: 15px;color:red;">
                 <asp:Label ID="Label1" runat="server" Text=""></asp:Label></span>
             <span style=" float:right;">
                 <asp:Button ID="Button2" runat="server" Text="确认" BackColor="#FFCCCC" BorderStyle="Groove" BorderWidth="1px" Height="29px" Width="80px" OnClick="Button2_Click" />

             </span>
                </h3>
                
            </div>

          </div>
        </div>
</asp:Content>

