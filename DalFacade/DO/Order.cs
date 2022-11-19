﻿namespace DO;

/// Structure for representing  an order of a customer.
public struct Order
    //מחלקה עבור הישות הזמנה - מתאר הזמנה של לקוח
{
    //שדה המציין האם ההזמנה תקפה או מחוקה
    public bool IsDeleted { get; set; } //    //שדה המציין את המס המזהה את ההזמנה
    public int ID { get; set; }      //שדה עבור שם המזמין
    public string? CustomerName { get; set; }    //כתובת המייל של המזמין
    public string? CustomerEmail { get; set; }      //כתובת המגורים של המזמין
    public string? CustomerAdress { get; set; }       //תאריך ביצוע ההזמנה
    public DateTime? OrderDate { get; set; }  // תאריך ביצוע ההזמנה
    public DateTime? shipDate { get; set; }  //תאריך משלוח
    public DateTime? DeliveryrDate { get; set; }  //תאריך מסירה
   
    //מתודת הפיכת הזמנה למחרוזת
    public override string ToString() => $@"  
	Order ID: {ID}, 
	CustomerName: {CustomerName}
    	CustomerEmail: {CustomerEmail}
    	CustomerAdress: {CustomerAdress}
    	OrderDate: {OrderDate}
    	shipDate: {shipDate}
    	DeliveryrDate: {DeliveryrDate}

	";
}
