using System;

namespace RepositorioAdapter.Repositorio
{
    public interface IRepositorio : IDisposable
    {
        // No todos los repositorios necesitar�n 
        // implementar el set completo de acciones del CRUD. En algunos sitios no desearemos 
        // que el usuario pueda borrar, actualizar,... Las acciones no necesarias suponen puertas 
        // traseras para entrar en la aplicaci�n y hacer acciones que se saltan las reglas de 
        // negocio establecidas o la seguridad e integridad de los datos. 
        // Adem�s el n�mero de bugs es directamente proporcional al n�mero de l�neas programadas. 
        // Por eso cada repositorio debe implementar tan solo el n�mero m�nimo de acciones 
        // que necesita cada repositorio.
    }
}