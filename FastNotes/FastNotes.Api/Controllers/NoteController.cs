using FastNotes.Api.DataTransferModels;
using FastNotes.Common.Models;
using FastNotes.Common.Models.Responses;
using FastNotes.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FastNotes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteWorker _noteWorker;

        public NoteController(INoteWorker noteWorker)
        {
            _noteWorker = noteWorker;
        }

        /// <summary>
        ///     Realiza a exclusão de uma nota pelo ID informado
        /// </summary>
        /// <param name="id">Parâmetro do tipo inteiro 
        /// utilizado como chave na exclusão</param>
        [HttpDelete]
        public async Task<GenericResponse<Note>> Delete(int id)
        {
            return await _noteWorker.Delete(id);
        }

        /// <summary>
        ///     Realiza a listagem das notas de acordo com o ID do usuário logado juntamente com 
        /// o ID da categoria em questão
        /// </summary>
        /// <param name="userId">Parâmetro do tipo inteiro para filtro de listagem</param>
        /// <param name="categoryId">Parâmetro do tipo inteiro para filtro de listagem</param>
        [HttpGet]
        public async Task<GenericResponse<IEnumerable<Note>>> List(int userId, int categoryId)
        {
            return await _noteWorker.List(userId, categoryId);
        }

        /// <summary>
        ///     Atualiza uma nota cadastrada, respeitando também o controle por usuário
        /// </summary>
        /// <param name="note">Parâmetro do tipo composto "Note"</param>
        [HttpPut]
        public async Task<GenericResponse<Note>> Update(Note note)
        {
            return await _noteWorker.Update(note);
        }

        /// <summary>
        ///     Cadastra uma nova nota, desde que a mesma sejá válida e com algum conteúdo, também
        /// é validado se a mesma possui arquivos vinculados e realiza-se a ordenação.
        /// </summary>
        /// <param name="note">Parâmetro do tipo composto "Note"</param>
        /// <returns>Retorna a criação de uma nova nota com tiítulo, conteúdo, categoria e os possíveis arquivos</returns>
        [HttpPost]
        public async Task<GenericResponse<Note>> Write(NoteDto note)
        {
            //var noteFiles = new List<NoteFile>();

            //if (note.NoteFiles != null && note.NoteFiles.Any())
            //{
            //    var order = 0;
            //    foreach (var file in note.NoteFiles)
            //    {
            //        noteFiles.Add(new NoteFile()
            //        {
            //            NoteId = file.NoteId,
            //            Order = order++,
            //            ContentBase64 = file.contentBase64
            //        });
            //    }
            //}
                
            return await _noteWorker.Write(
                new Note()
                {
                    Title = note.Title,
                    Content = note.Content,
                    CategoryId = note.CategoryId,
                    FileBase64 = note.FileContent64,
                    Priority = note.Priority,
                    Status = note.Status
                });
        }
    }
}
