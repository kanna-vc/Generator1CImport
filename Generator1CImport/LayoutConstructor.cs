using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator1CImport
{
    class LayoutConstructor
    {
        public string Header()
        {
            return Header("Загрузить");
        }
        public string Header(string ButtonStart)
        {
            string Result = @"&НаКлиенте
Процедура " + ButtonStart + "(Команда)";
            Result += @"
	ДиалогВыбораФайла = Новый ДиалогВыбораФайла(РежимДиалогаВыбораФайла.Открытие);
	ДиалогВыбораФайла.Фильтр = " + "\"" + "Файлы CSV(*.csv) | *.csv" + "\"" + @";

	Если диалогВыбораФайла.Выбрать() Тогда
		Путь = ДиалогВыбораФайла.ПолноеИмяФайла;
		ЗаписатьВСправочник(Путь);
	КонецЕсли;
КонецПроцедуры

&НаСервере
Процедура ЗаписатьВСправочник(АдресХранилища)

	ТекстовыйДокумент = Новый ТекстовыйДокумент;
	ТекстовыйДокумент.Прочитать(АдресХранилища);
            ";
            return Result;
        }

        public string Head(string StartLane, string Directory)
        {
            return Head(StartLane, ";", Directory);
        }
        public string Head(string StartLane, string Separator, string Directory)
        {
            string Result = @"
	Для НомерСтроки = " + StartLane + @" По ТекстовыйДокумент.КоличествоСтрок() Цикл;
		Строка = ТекстовыйДокумент.ПолучитьСтроку(НомерСтроки);
		мсвЭлементовСтроки = СтрРазделить(Строка, " + "\"" + Separator + "\"" + @",Истина);

		ЭлементСправочника  = Справочники." + Directory + ".СоздатьЭлемент();";
            return Result;
        }

        public string Body(string Requisites, string TypeReq, string Line)
        {
            string Result = " ";

            if (TypeReq.StartsWith("Справочники."))
            {
                Result = @"
		" + "ЭлементСправочника." + Requisites + " = " + TypeReq + ".НайтиПоНаименованию(мсвЭлементовСтроки[" + Line + "]);";
            }
            else if (TypeReq.StartsWith("Дата"))
            {
                Result = @"
		" + "ЭлементСправочника." + Requisites + " = КонвертДаты(мсвЭлементовСтроки[" + Line + "]);";
            }
            else
            {
                Result = @"
		" + "ЭлементСправочника." + Requisites + " = мсвЭлементовСтроки[" + Line + "];";
            }
            return Result;
        }

        public string Footer()
        {
            string Result = @"

		Попытка 
			ЭлементСправочника.Записать();
		Исключение
			Сообщить(ОписаниеОшибки());
		КонецПопытки;
	КонецЦикла;
КонецПроцедуры";
            return Result;
        }

        public string DataConvert()
        {
            string Result = @"

Функция КонвертДаты(Дата)
	Возврат Строка(Прав(Дата,4) + Сред(Дата,4,2) + Лев(Дата,2));	
КонецФункции";
            return Result;
        }
    }
}
