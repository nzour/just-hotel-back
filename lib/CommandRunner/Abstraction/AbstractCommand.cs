using CommandRunner.Handler;

namespace CommandRunner.Abstraction
{
    public abstract class AbstractCommand
    {
        /// <summary>
        ///     Реализация команды.
        /// </summary>
        /// <param name="provider">Поставляет аргументы из консоли</param>
        public abstract void Execute(ArgumentProvider provider);

        /// <summary>
        ///     Задает название команды, чтобы использовать его для вызова их консоли.
        ///     Команды не могут иметь повторяющиеся названия.
        /// </summary>
        /// <returns>Вернет название команды</returns>
        public abstract string GetName();

        /// <summary>
        ///     Описание команды или null, при его отсутствии.
        /// </summary>
        public virtual string? GetDescription()
        {
            return null;
        }
    }
}