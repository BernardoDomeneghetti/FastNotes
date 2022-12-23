using FastNotes.Common.Models.Responses;
using FastNotes.Common.Models;
using FastNotes.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FastNotes.Api.DataTransferModels;

namespace FastNotes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryWorker _categoryWorker;

        public CategoryController(ICategoryWorker categoryWorker)
        {
            _categoryWorker = categoryWorker;
        }

        /// <summary>
        ///     Realiza a exclusão da categoria correspondente ao ID informado
        /// </summary>
        /// <param name="id">Parâmetro do tipo inteiro 
        /// utilizado como chave na exclusão</param>
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _categoryWorker.Delete(id);
        }

        /// <summary>
        ///     Faz a listagem de todas as categorias que o usuário informado pode visualizar
        /// </summary>
        /// <param name="userId">Parâmetro do tipo inteiro 
        /// usado para filtrar as categorias do usuário em questão</param>
        [HttpGet]
        public async Task<GenericResponse<List<Category>>> List(int userId)
        {
            return await _categoryWorker.List(userId);
        }

        /// <summary>
        ///     Cadastra uma nova categoria de acordo com o nome informado e o ID do usuário
        /// que está realizando a ação 
        /// </summary>
        /// <param name="category">Parâmetro do tipo composto "CategoryDto"</param>
        /// <returns>Retorna uma categoria com nome e o usuário relativo</returns>
        [HttpPost]
        public async Task<GenericResponse<Category>> Register(CategoryDto category)
        {            
            return await _categoryWorker.Register(
                    new Category() 
                    { 
                        Name = category.Name,
                        UserId = category.UserId,
                    }
                );
        }

        /// <summary>
        ///     Atualiza uma categoria cadastrada, respeitando também o controle por usuário
        /// </summary>
        /// <param name="category">Parâmetro do tipo composto "CategoryDto"</param>
        [HttpPut]
        public async Task<GenericResponse<Category>> Update(Category category)
        {
            return await _categoryWorker.Update(category);
        }
    }
}
