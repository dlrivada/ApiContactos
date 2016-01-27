using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ContactosModel.Model;

namespace RepositorioAdapter.Repositorio
{
    // Esta interfaz implementa un buen monton de acciones que deberán implementar 
    // todos los repositorios que deriven de ella. No todos los repositorios necesitarán 
    // implementar el set completo de acciones del CRUD. En algunos sitios no desearemos 
    // que el usuario pueda borrar, actualizar,... Las acciones no necesarias suponen puertas 
    // traseras para entrar en la aplicación y hacer acciones que se saltan las reglas de 
    // negocio establecidas o la seguridad e integridad de los datos. 
    // Además el número de bugs es directamente proporcional al número de líneas programadas. 
    // Por eso cada repositorio debe implementar tan solo el número mínimo de acciones 
    // que necesita cada repositorio.
    // Es necesario impedir consultas del tipo "Dame todos los Usuarios", 
    // hay que ordenar siempre los resultados 
    // y paginarlos para que no repliquemos grandes cantidades de la DDBB en la memoria.
    public interface IRepositorioCanRead<TModel> where TModel : DomainModel
    {
        TModel Get(params object[] keys);
        // TODO: Habría que implementar paginación y ordenación de datos
        ICollection<TModel> Get(Expression<Func<TModel, bool>> expression);
        // TODO: Este es un ejemplo de método no permitido que habría que eliminar y sustituir por búsquedas filtradas siempre
        ICollection<TModel> Get();
    }
}