﻿using BlazorEcommerce.Shared.Category;

namespace BlazorEcommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;
        private const string CategoryBaseURL = "api/Category/";
        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public List<CategoryDto> AdminCategories { get; set; } = new List<CategoryDto>();

        public event Action OnChange;

        public async Task AddCategory(CategoryDto CategoryDto)
        {
            var response = await _http.PostAsJsonAsync($"{CategoryBaseURL}admin", CategoryDto);
            var result = (await response.Content
                .ReadFromJsonAsync<IResponse>());

            if (result != null && result.Success)
            {
                var responseCast = (DataResponse<List<CategoryDto>>)result;

                AdminCategories = responseCast.Data;

                await GetCategories();

                OnChange.Invoke();
            }
        }

        public CategoryDto CreateNewCategory()
        {
            var newCategoryDto = new CategoryDto { IsNew = true, Editing = true };
            AdminCategories.Add(newCategoryDto);
            OnChange.Invoke();
            return newCategoryDto;
        }

        public async Task DeleteCategory(int CategoryDtoId)
        {
            var response = await _http.DeleteAsync($"{CategoryBaseURL}admin/{CategoryDtoId}");

            var result = (await response.Content
               .ReadFromJsonAsync<IResponse>());

            if (result != null && result.Success)
            {
                var responseCast = (DataResponse<List<CategoryDto>>)result;

                AdminCategories = responseCast.Data;

                await GetCategories();

                OnChange.Invoke();
            }
        }

        public async Task GetAdminCategories()
        {
            var response = await _http.GetFromJsonAsync<IResponse>($"{CategoryBaseURL}admin");
            if (response != null && response.Success)
            {
                var responseCast = (DataResponse<List<CategoryDto>>)response;
                AdminCategories = responseCast.Data;
            }

        }

        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<IResponse>($"{CategoryBaseURL}");
            if (response != null && response.Success)
            {
                var responseCast = (DataResponse<List<CategoryDto>>)response;
                Categories = responseCast.Data;
            }
        }

        public async Task UpdateCategory(CategoryDto CategoryDto)
        {
            var response = await _http.PutAsJsonAsync($"{CategoryBaseURL}admin", CategoryDto);
            var result  = (await response.Content
                .ReadFromJsonAsync<IResponse>());

            if (result != null && result.Success)
            {
                var responseCast = (DataResponse<List<CategoryDto>>)result;

                AdminCategories = responseCast.Data;

                await GetCategories();

                OnChange.Invoke();
            }
        }
    }
}
