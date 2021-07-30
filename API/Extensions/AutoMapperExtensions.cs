using System.Collections.Generic;
using AutoMapper;

namespace API.Extensions
{
    public static class AutoMapperExtensions<T, U> 
    {
        public static List<U> MapList(IMapper mapper, List<T> source )
        {
            var destination = new List<U>();

            foreach (var sourceElem in source)
            {
                destination.Add(mapper.Map<U>(sourceElem));
            }

            return destination;

        }
    }
}