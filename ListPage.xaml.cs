using Todea_Denisa_Lab7.Models;

namespace Todea_Denisa_Lab7;

public partial class ListPage : ContentPage
{
	public ListPage()
	{
		InitializeComponent();
	}
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)
       this.BindingContext)
        {
            BindingContext = new Product()
        });

    }
    public Task<int> SaveListProductAsync(ListProduct listp)
    {
        if (listp.ID != 0)
        {
            return _database.UpdateAsync(listp);
        }
        else
        {
            return _database.InsertAsync(listp);
        }
    }
    public Task<List<Product>> GetListProductsAsync(int shoplistid)
    {
        return _database.QueryAsync<Product>(
        "select P.ID, P.Description from Product P"
        + " inner join ListProduct LP"
        + " on P.ID = LP.ProductID where LP.ShopListID = ?",
        shoplistid);
    }
}