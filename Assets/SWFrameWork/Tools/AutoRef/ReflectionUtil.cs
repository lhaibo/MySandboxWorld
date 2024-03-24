﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace SWFrameWork.Tools.AutoRef
{
    public static class ReflectionUtil
    {
        public struct AttributedField<T>
            where T : Attribute
        {
            public T Attribute;
            public FieldInfo FieldInfo;
        }

        public static void GetFieldsWithAttributeFromType<T>(
            Type classToInspect,
            IList<AttributedField<T>> output,
            BindingFlags reflectionFlags = BindingFlags.Default
        )
            where T : Attribute
        {
            Type type = typeof(T);
            do
            {
                FieldInfo[] allFields = classToInspect.GetFields(reflectionFlags);
                for (int f = 0; f < allFields.Length; f++)
                {
                    FieldInfo fieldInfo = allFields[f];
                    Attribute[] attributes = Attribute.GetCustomAttributes(fieldInfo);
                    for (int a = 0; a < attributes.Length; a++)
                    {
                        Attribute attribute = attributes[a];
                        if (!type.IsInstanceOfType(attribute))
                            continue;

                        output.Add(new AttributedField<T>
                        {
                            Attribute = attribute as T,
                            FieldInfo = fieldInfo
                        });
                        break;
                    }
                }

                classToInspect = classToInspect.BaseType;
            } while (classToInspect != null);
        }
    }
}