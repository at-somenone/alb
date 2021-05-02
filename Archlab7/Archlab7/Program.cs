using System;
using Word = Microsoft.Office.Interop.Word;

namespace Archlab7
{
    class Program
    {
        private static void Main() {
            Console.Write("Имя кафедры: ");
            var faculty = Console.ReadLine();
            Console.Write("Номер лабораторной работы: ");

            int labNumber;
            while (!int.TryParse(Console.ReadLine(), out labNumber))
                Console.Write("Неверное число: ");

            Console.Write("Тема лабораторной работы: ");
            var topic = Console.ReadLine(); // todo should be topic

            Console.Write("Имя дисциплины: ");
            var discipline = Console.ReadLine();

            Console.Write("Имя проверяющего: ");
            var professor = Console.ReadLine();

            Console.Write("Год: ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year))
                Console.Write("Неверное число: ");

            var app = new Word.Application().Application.Application.Application.Application.Application;
            app.Visible = true;

            var doc = app.Documents.Add();

            doc.PageSetup.TopMargin = CentimetersToPoints(2);
            doc.PageSetup.BottomMargin = CentimetersToPoints(2);
            doc.PageSetup.LeftMargin = CentimetersToPoints(3);
            doc.PageSetup.RightMargin = CentimetersToPoints(1.5f);

            var paragraph = doc.Content;
            doc.Content.Paragraphs.SpaceAfter = 0;
            doc.Content.Paragraphs.Space1();
            doc.Content.Font.Size = 14;
            doc.Content.Font.Name = "Times New Roman";
            doc.Content.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            NewParagraph(paragraph);
            paragraph.Text = "МИНИСТЕРСТВО НАУКИ И ВЫСШЕГО ОБРАЗОВАНИЯ РОССИЙСКОЙ ФЕДЕРАЦИИ ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ «ОРЛОВСКИЙ ГОСУДАРСТВЕННЫЙ УНИВЕРСИТЕТ ИМЕНИ И.С.ТУРГЕНЕВА»";
            paragraph.Text += Newlines(1);
            paragraph.Text += $"Кафедра {faculty}{Newlines(3)}";

            NewParagraph(paragraph);
            paragraph.Font.Size = 16;
            paragraph.Font.Bold = 1;
            paragraph.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph.Text += "ОТЧЁТ";

            NewParagraph(paragraph);
            paragraph.Bold = 0;
            paragraph.Text += "По лабораторной работе №" + labNumber;
            paragraph.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            NewParagraph(paragraph);
            paragraph.Paragraphs.SpaceAfter = 10;
            paragraph.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph.Text += $"на тему: «{topic}»{Newlines(1)}";
            paragraph.Text += $"по дисциплине: «{discipline}»{Newlines(8)}";

            NewParagraph(paragraph);
            paragraph.Text += "Выполнили: Яковлев И. В., Никулин А. А." + Newlines(1);
            paragraph.Text += "Институт приборостроения, автоматизации и информационных технологий" + Newlines(1);
            paragraph.Text += "Направление: 09.03.04 «Программная инженерия»" + Newlines(1);
            paragraph.Text += "Группа: 92ПГ" + Newlines(1);
            paragraph.Text += "Проверил: " + professor + Newlines(1);
            paragraph.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            NewParagraph(paragraph);
            paragraph.Text += "Отметка о зачете: ";
            paragraph.Paragraphs.SpaceAfter = 10;
            paragraph.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            NewParagraph(paragraph);
            paragraph.Text += $"Дата: «____» __________ {year}г.";
            paragraph.Paragraphs.SpaceAfter = 0;
            paragraph.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;

            NewParagraph(paragraph);
            NewParagraph(paragraph);
            paragraph.Text += Newlines(8) + "Орел, " + year;
            paragraph.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
        }

        private static void NewParagraph(Word.Range paragraph) {
            paragraph.Collapse(Word.WdCollapseDirection.wdCollapseEnd);
            paragraph.InsertParagraph();
        }

        private static string Newlines(int count) {
            return new('\n', count);
        }

        private static float CentimetersToPoints(float cm) {
            return cm * 28.3465f;
        }
    }
}
