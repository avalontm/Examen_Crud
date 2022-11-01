
using Examen.Database.Tables;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

public enum Genero
{
    Hombre,
    Mujer
}

public class Generos
{
    public string value { set; get; }
    public string text { set; get; }
    public bool selected { set; get; }
}
public static class ListGeneros
{
    static List<Generos> _items;
    public static List<Generos> items {

        get {

            if (_items == null)
            {
                _items = new List<Generos>()
                {
                    new Generos() {value = "0", text = "Hombre" },
                    new Generos() {value = "1", text = "Mujer" }
                };
            }

            return _items;
        }
    }

    public static IEnumerable<SelectListItem> ItemSelect(string value)
    {
        List<SelectListItem> sItems = new List<SelectListItem>();
        foreach (var item in items)
        {
            SelectListItem sItem = new SelectListItem();
            sItem.Value = item.value;
            sItem.Text = item.text; 

            if (item.value == value)
            {
                item.selected = true;
            }

            sItem.Selected = item.selected;

            sItems.Add(sItem);
        }

        return sItems;
    }

}

public static class ListProfesores
{
    public static IEnumerable<SelectListItem> ItemSelect(string value)
    {
        var items = Profesor.Get();
        List<SelectListItem> sItems = new List<SelectListItem>();
        foreach (var item in items)
        {
            SelectListItem sItem = new SelectListItem();
            sItem.Value = item.id.ToString();
            sItem.Text = string.Format("{0} {1}",item.nombre, item.apellidos);

            if (sItem.Value == value)
            {
                sItem.Selected = true;
            }

            sItems.Add(sItem);
        }

        return sItems;
    }
}

public static class ListAlumnos
{
    public static IEnumerable<SelectListItem> ItemSelect(string value)
    {
        var items = Alumno.Get();
        List<SelectListItem> sItems = new List<SelectListItem>();
        foreach (var item in items)
        {
            SelectListItem sItem = new SelectListItem();
            sItem.Value = item.id.ToString();
            sItem.Text = string.Format("{0} {1}", item.nombre, item.apellidos);

            if (sItem.Value == value)
            {
                sItem.Selected = true;
            }

            sItems.Add(sItem);
        }

        return sItems;
    }
}

public static class ListGrados
{
    public static IEnumerable<SelectListItem> ItemSelect(string value)
    {
        var items = Grado.Get();
        List<SelectListItem> sItems = new List<SelectListItem>();
        foreach (var item in items)
        {
            SelectListItem sItem = new SelectListItem();
            sItem.Value = item.id.ToString();
            sItem.Text = string.Format("{0}", item.nombre);

            if (sItem.Value == value)
            {
                sItem.Selected = true;
            }

            sItems.Add(sItem);
        }

        return sItems;
    }
}