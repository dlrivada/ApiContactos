using System;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorio : IDisposable
    {
        // No todos los repositorios necesitarán 
        // implementar el set completo de acciones del CRUD. En algunos sitios no desearemos 
        // que el usuario pueda borrar, actualizar,... Las acciones no necesarias suponen puertas 
        // traseras para entrar en la aplicación y hacer acciones que se saltan las reglas de 
        // negocio establecidas o la seguridad e integridad de los datos. 
        // Además el número de bugs es directamente proporcional al número de líneas programadas. 
        // Por eso cada repositorio debe implementar tan solo el número mínimo de acciones 
        // que necesita cada repositorio.
    }
}