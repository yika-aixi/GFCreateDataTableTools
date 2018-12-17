# GFCreateDataTableTools
GF数据表生成工具

主要的的代码为`CSVDataRow`类中的`IDataRowCreate`接口实现配合`FieldComment`特性来实现的创建

IDataRowCreate接口不应该在Runtime里,应该在Editor,可自行移出去,希望我的思路可以给到你帮助
