using ApplicationCore.Entities;
using MrDelivery.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrDelivery.Services
{
    public class CartViewModelService
    {
        public CartViewModel CreateViewModelFromCart(Cart cart)
        {
            var viewModel = new CartViewModel();
            viewModel.Id = cart.Id;
            viewModel.Quantity = cart.Quantity;
            viewModel.dateCreated = cart.dateCreated;

            return viewModel;

        }
    }
}
