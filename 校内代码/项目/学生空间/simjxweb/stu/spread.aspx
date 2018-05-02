<%@ Page Title="" Language="C#" MasterPageFile="~/stu/LX.master" AutoEventWireup="true" CodeFile="spread.aspx.cs" Inherits="stu_spread" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <style>
    </style>
    <div style=" font-size: 14px;
    height: 58px;
    line-height: 58px;
    margin-top: 10px;
    border-bottom: 1px solid #d0d6d9;
    border-bottom-width: 1px;
    border-bottom-style: solid;
    border-bottom-color: rgb(208, 214, 217);">

        <div style="float:right;font-size:15px">分享</div>
      
      </div>
      <div style="font-size:30px;height:100px;text-align:center;line-height:100px;background-color:lightpink">已获积分：
          </div>
      
        <div style="font-size:20px;height:30px;background-color:cornflowerblue;">
        <div style="float:left;width:33.33%;margin-left:20px">已推荐</div>
        <div style="float:left;margin-left:0px;width:33.33%">已成功</div>
        <div style="float:left;margin-left:0px">已失效</div>
        </div>
      <div style="font-size:20px;height:30px;background-color:cornflowerblue">
            <div style="float:left;width:33.33%;margin-left:30px">0</div>
        <div style="float:left;margin-left:10px;width:33.33%;">0</div>
        <div style="float:left;margin-left:10px">0</div>
      </div>
     <div style=" font-size: 14px;
    height: 58px;
    line-height: 58px;
    margin-top: 10px;
    border-bottom: 1px solid #d0d6d9;
    border-bottom-width: 1px;
    border-bottom-style: solid;
    border-bottom-color: rgb(208, 214, 217);">
         </div>
    <div>
         <table border="1" style="width: 100%;margin:auto;font-size:20px";>
             <tr style=" text-align:center; line-height:45px">
                 <th>邀请方式</th>
                  <th>时间</th>
                  <th>地点</th>
                  <th>好友</th>
                  <th>是否成功</th>
             </tr>
               <tr >
                 <td></td>
                   <td></td>
                   <td></td>
                   <td><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
                   <td></td>
             </tr>
         </table>
     </div>
</asp:Content>

