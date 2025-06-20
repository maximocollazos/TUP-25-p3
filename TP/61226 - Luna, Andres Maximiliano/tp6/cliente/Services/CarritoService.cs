using cliente.Models;

namespace cliente.Services;

public class CarritoService
{
    private List<ItemCarrito> _items = new();

    public List<ItemCarrito> ObtenerCarrito() => _items;

    public void AgregarAlCarrito(Producto producto)
{
    var item = _items.FirstOrDefault(i => i.Producto.Id == producto.Id);
    
    if (item != null)
    {
        
        if (producto.StockDisponible > 0)
        {
            item.Cantidad++;
            producto.StockDisponible--;
        }
    }
    else
    {
        if (producto.StockDisponible > 0)
        {
            _items.Add(new ItemCarrito
            {
                Producto = producto,
                Cantidad = 1
            });
            producto.StockDisponible--;
        }
    }
}



    public void AumentarCantidad(int productoId)
{
    var item = _items.FirstOrDefault(i => i.Producto.Id == productoId);
    if (item != null && item.Producto.StockDisponible > 0)
    {
        item.Cantidad++;
        item.Producto.StockDisponible--;
    }
}


    public void DisminuirCantidad(int productoId)
    {
    var item = _items.FirstOrDefault(i => i.Producto.Id == productoId);
        if (item != null && item.Cantidad > 0)
        {
            item.Cantidad--;
            item.Producto.StockDisponible++;

            if (item.Cantidad == 0)
            {
                _items.Remove(item);
            }
        }
}   


    public void EliminarDelCarrito(int productoId)
    {
        var item = _items.FirstOrDefault(i => i.Producto.Id == productoId);
        if (item != null)
            _items.Remove(item);
    }

    public void VaciarCarrito()
    {
        foreach (var item in _items)
        {
            item.Producto.Stock += item.Cantidad;
            item.Producto.StockDisponible += item.Cantidad;
        }

        _items.Clear();
    }

    public decimal CalcularTotal() =>
        _items.Sum(i => i.Cantidad * i.Producto.Precio);

    public int ObtenerCantidadTotalEnCarrito()
    {
        return _items.Sum(p => p.Cantidad);
    } 
}
