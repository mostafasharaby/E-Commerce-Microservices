﻿using InventoryService.Domain.Enums;

namespace InventoryService.Domain.Entities;

public class InventoryItem
{
    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int QuantityAvailable { get; private set; }
    public int QuantityReserved { get; private set; }
    public int LowStockThreshold { get; private set; }
    public StockStatus Status { get; private set; }

    public InventoryItem(int productId, int quantityAvailable, int lowStockThreshold)
    {
        ProductId = productId;
        QuantityAvailable = quantityAvailable;
        QuantityReserved = 0;
        LowStockThreshold = lowStockThreshold;
        UpdateStatus();
    }

    public void ReserveStock(int quantity)
    {
        if (QuantityAvailable < quantity) throw new Exception("Insufficient stock");
        QuantityAvailable -= quantity;
        QuantityReserved += quantity;
        UpdateStatus();
    }

    public void ReleaseStock(int quantity)
    {
        if (QuantityReserved < quantity) throw new Exception("Insufficient stock");
        QuantityReserved -= quantity;
        QuantityAvailable += quantity;
        UpdateStatus();
    }

    public void UpdateStock(int quantity)
    {
        QuantityAvailable += quantity;
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        Status = QuantityAvailable <= 0 ? StockStatus.OutOfStock :
                 QuantityAvailable <= LowStockThreshold ? StockStatus.LowStock :
                 StockStatus.InStock;
    }
}

