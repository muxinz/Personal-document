<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="set_edit.aspx.cs" Inherits="stu_set" %>

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
                border-bottom-color: rgb(240, 20, 0)">安全设置</span>
             </div>
        <div style="float: right;">
            <div style="position: relative;margin-right: 10px;">
                <span class="cursor: pointer"><a href="#">编辑</a></span>
            </div>

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
     <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 昵 称:
                    <asp:TextBox ID="TextBox1" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px"></asp:TextBox>
                </h3>
            </div>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 性 别:
                    <asp:TextBox ID="TextBox2" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px"></asp:TextBox>
                    <span style=" font-size: 15px;color:red;">（*必填）</span>
                </h3>
            </div>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 年 级:
                    <asp:TextBox ID="TextBox3" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px"></asp:TextBox>
                    <span style=" font-size: 15px;color: red;">（*必填）</span>
                </h3>
            </div>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 学 校:
                    <asp:TextBox ID="TextBox4" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px"></asp:TextBox>
                </h3>
            </div>
            <div style="padding: 30px 0;padding-top: 30px;padding-right: 0px;padding-bottom: 30px;padding-left: 0px;position: relative;border-bottom: 1px solid #eff1f0;border-bottom-width: 1px; border-bottom-style: solid;border-bottom-color: rgb(239, 241, 240);">
         <h3 style="font-size: 20px;color: #12171b;height: 29px;line-height: 29px;position: relative;"> 签 名:
                    <asp:TextBox ID="TextBox5" runat="server" BorderColor="Black" Height="26px" BackColor="White" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
             <span style=" float:right;">
                 
<%--                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                     <ContentTemplate>--%>
                 <asp:Button ID="Button2" runat="server" Text="确认" BackColor="#FFCCCC" BorderStyle="Groove" BorderWidth="1px" Height="29px" Width="80px" OnClick="Button2_Click" />
<%--                      </ContentTemplate>
                 </asp:UpdatePanel>--%>
             </span>
                </h3>
                
            </div>

          </div>
        </div>

</asp:Content>

