<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="set.aspx.cs" Inherits="stu_set_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
            <div style="position: relative;margin-right: 10px;">
                <span class="cursor: pointer"><a href="set_edit.aspx?stulogname=<%=a %>&stuschool=<%=b %>&sex=<%=c %>&stugrade=<%=d %>">编辑</a></span>
            </div>

        </div>
    </div>

            
    <div>
        <style>
            .kk
            {
               text-align:center;
               font-size:20px;
               line-height:50px;
               margin-left:10px;
            }
            .ff
            {
                 float:left;
                 height:50px;
                 width:20%;
                 background-color:lightgreen;
                 text-align:center;
                 line-height:50px
            }
            .gg 
            {
                     float:left;
                     height:50px;
                     width:80%; 
                     border-bottom:1px solid #000000;
                     border-bottom-color:aquamarine      
            }          
              </style>
        <div style="height:50px;">
            <div class="ff">昵称：</div>
            <div class="gg"><asp:Label id="stulogname" runat="server" CssClass="kk" Font-Size="Medium" Text=""></asp:Label></div>
        </div>
            
        <br />
         <div style="height:50px;">
            <div class="ff">学校：</div>
            <div class="gg"><asp:Label id="stuschool" runat="server" CssClass="kk" Font-Size="Medium" Text=""></asp:Label></div>
        </div>
        <br />
    <div style="height:50px;">
            <div class="ff">性别：</div>
            <div class="gg"><asp:Label id="stusex" runat="server" CssClass="kk" Font-Size="Medium" Text=""></asp:Label></div>
        </div>
        <br />
    <div style="height:50px;">
            <div class="ff">个性签名：</div>
            <div class="gg"><asp:Label runat="server" CssClass="kk " Font-Size="Medium">吴彦祖没我帅</asp:Label></div>
        </div>
           
   <br />
    <br />
        <br />    
    </div>
 
</asp:Content>

