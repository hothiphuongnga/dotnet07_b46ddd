namespace SalesDb.Domain.Common;

// mảket interface:  không có method, chỉ dùng để đánh dấu 
public interface IAggregateRoot{}

public class AggregateRoot<Tkey> : BaseEntity<Tkey>, IAggregateRoot{}


//  Ordẻ => ordertail
//  AggregateRoot: cổng duy nhất để thay dự liệu entity nào đó