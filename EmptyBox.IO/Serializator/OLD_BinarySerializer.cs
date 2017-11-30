//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Reflection;
//using System.IO;
//using System.Runtime.CompilerServices;
//using EmptyBox.Utils;

//namespace EmptyBox.IO.Serializator
//{
//    public class BinarySerializer
//    {
//        public static Dictionary<Type, int> Sizes { get; private set; }
//        static BinarySerializer()
//        {
//            Sizes = new Dictionary<Type, int>();
//            Sizes.Add(typeof(bool), sizeof(bool));
//            Sizes.Add(typeof(byte), sizeof(byte));
//            Sizes.Add(typeof(sbyte), sizeof(sbyte));
//            Sizes.Add(typeof(short), sizeof(short));
//            Sizes.Add(typeof(ushort), sizeof(ushort));
//            Sizes.Add(typeof(int), sizeof(int));
//            Sizes.Add(typeof(uint), sizeof(uint));
//            Sizes.Add(typeof(long), sizeof(long));
//            Sizes.Add(typeof(ulong), sizeof(ulong));
//            Sizes.Add(typeof(float), sizeof(float));
//            Sizes.Add(typeof(double), sizeof(double));
//        }

//        public Encoding TextEncoding { get; protected set; }

//        public BinarySerializer(Encoding enc)
//        {
//            TextEncoding = enc;
//        }

//        public byte[] Serialize(dynamic var)
//        {
//            int length = GetLength(var);
//            byte[] data = new byte[length];
//            MemoryStream ms = new MemoryStream(data);
//            BinaryWriter bwms = new BinaryWriter(ms);
//            Serialize(bwms, var);
//            bwms.Flush();
//            data = ms.ToArray();
//            bwms.Dispose();
//            ms.Dispose();
//            return data;
//        }

//        public void Serialize(Stream data, dynamic var)
//        {
//            int length = GetLength(var);
//            BinaryWriter bwms = new BinaryWriter(data);
//            Serialize(bwms, var);
//            bwms.Flush();
//        }

//        public void Serialize(BinaryWriter bwms, dynamic var)
//        {
//            if (!TypeExtensions.CanBeNull(var?.GetType()) || var != null)
//            {
//                bwms.Write(false);
//                Type[] generictypes = var.GetType().GenericTypeArguments;
//                if (var is Array && !(var is byte[]))
//                {
//                    uint arank = (uint)var.Rank;
//                    bwms.Write(arank);
//                    uint alength = (uint)var.Length;
//                    bwms.Write(alength);
//                    if (!Sizes.ContainsKey(var.GetType().GetElementType()))
//                    {
//                        for (int i0 = 0; i0 < alength; i0++)
//                        {
//                            Serialize(bwms, var[i0]);
//                        }
//                    }
//                    else
//                    {
//                        for (int i0 = 0; i0 < alength; i0++)
//                        {
//                            bwms.Write(var[i0]);
//                        }
//                    }
//                }
//                else if (var is byte[])
//                {
//                    bwms.Write((uint)var.Length);
//                    bwms.Write(var);
//                }
//                else if (generictypes.Length == 1 && typeof(IEnumerable<>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(var.GetType()))
//                {
//                    bwms.Write((uint)var.Count);
//                    for (int i0 = 0; i0 < var.Count; i0++)
//                    {
//                        Serialize(bwms, Enumerable.ToList(var)[i0]);
//                    }
//                }
//                else if (generictypes.Length == 2 && typeof(IReadOnlyDictionary<,>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(var.GetType()))
//                {
//                    bwms.Write((uint)var.Count);
//                    foreach (dynamic pair in var)
//                    {
//                        Serialize(bwms, pair);
//                    }
//                }
//                else if (generictypes.Length == 2 && typeof(KeyValuePair<,>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(var.GetType()))
//                {
//                    Serialize(bwms, var.Key);
//                    Serialize(bwms, var.Value);
//                }
//                else if (Sizes.ContainsKey(var.GetType()))
//                {
//                    bwms.Write(var);
//                }
//                else if (var is char)
//                {
//                    bwms.Write(TextEncoding.GetBytes(new char[1] { var }).Length);
//                    bwms.Write(TextEncoding.GetBytes(new char[1] { var }));
//                }
//                else if (var is string)
//                {
//                    bwms.Write(TextEncoding.GetBytes(var).Length);
//                    bwms.Write(TextEncoding.GetBytes(var));
//                }
//                else if (var is Guid)
//                {
//                    bwms.Write(var.ToByteArray());
//                }
//                else if (var is DateTime)
//                {
//                    bwms.Write(var.ToBinary());
//                }
//                else if (var.GetType().IsEnum)
//                {
//                    Type enumtype = Enum.GetUnderlyingType(var.GetType().AsType());
//                    Serialize(bwms, Convert.ChangeType(var, enumtype));
//                }
//                else
//                {
//                    List<FieldInfo> var_fi;
//                    List<PropertyInfo> var_pi;
//                    var_fi = (var as object).GetType().GetTypeInfo().DeclaredFields.ToList().FindAll(x => !x.IsStatic && x.IsPublic);
//                    var_pi = (var as object).GetType().GetTypeInfo().DeclaredProperties.ToList().FindAll(x => x.CanRead && x.CanWrite);
//                    uint count = (uint)(var_fi.Count + var_pi.Count);
//                    bwms.Write(count);
//                    foreach (FieldInfo fi in var_fi)
//                    {
//                        Serialize(bwms, fi.GetValue(var));
//                    }
//                    foreach (PropertyInfo pi in var_pi)
//                    {
//                        Serialize(bwms, pi.GetValue(var));
//                    }
//                }
//            }
//            else
//            {
//                bwms.Write(true);
//            }
//        }

//        public int GetLength(dynamic var)
//        {
//            int r = Sizes[typeof(bool)];
//            if (!TypeExtensions.CanBeNull(var?.GetType()) || var != null)
//            {
//                Type[] generictypes = var.GetType().GenericTypeArguments;
//                if (var is Array)
//                {
//                    uint rank = (uint)var.Rank;
//                    r += Sizes[rank.GetType()];
//                    uint length = (uint)var.Length;
//                    r += Sizes[length.GetType()];
//                    if (!Sizes.ContainsKey(var.GetType().GetElementType()))
//                    {
//                        for (int i0 = 0; i0 < length; i0++)
//                        {
//                            r += GetLength(var[i0]);
//                        }
//                    }
//                    else
//                    {
//                        r += length * Sizes[var.GetType().GetElementType()];
//                    }
//                }
//                else if (var is byte[])
//                {
//                    r += var.Length + Sizes[typeof(uint)];
//                }
//                else if (generictypes.Length == 1 && typeof(IEnumerable<>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(var.GetType()))
//                {
//                    r += Sizes[typeof(uint)];
//                    for (int i0 = 0; i0 < var.Count; i0++)
//                    {
//                        r += GetLength(Enumerable.ToList(var)[i0]);
//                    }
//                }
//                else if (generictypes.Length == 2 && typeof(IReadOnlyDictionary<,>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(var.GetType()))
//                {
//                    r += Sizes[typeof(uint)];
//                    foreach (dynamic pair in var)
//                    {
//                        r += GetLength(pair);
//                    }
//                }
//                else if (generictypes.Length == 2 && typeof(KeyValuePair<,>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(var.GetType()))
//                {
//                    r += GetLength(var.Key);
//                    r += GetLength(var.Value);
//                }
//                else if (Sizes.ContainsKey(var.GetType()))
//                {
//                    r += Sizes[var.GetType()];
//                }
//                else if (var is char)
//                {
//                    r += Sizes[typeof(int)];
//                    r += TextEncoding.GetBytes(new char[1] { var }).Length;
//                }
//                else if (var is string)
//                {
//                    r += Sizes[typeof(int)];
//                    r += TextEncoding.GetBytes(var).Length;
//                }
//                else if (var is Guid)
//                {
//                    r += 16;
//                }
//                else if (var is DateTime)
//                {
//                    r += Sizes[typeof(long)];
//                }
//                else if (var.GetType().IsEnum)
//                {
//                    Type enumtype = Enum.GetUnderlyingType(var.GetType().AsType());
//                    r += GetLength(Convert.ChangeType(var, enumtype));
//                }
//                else
//                {
//                    List<FieldInfo> var_fi;
//                    List<PropertyInfo> var_pi;
//                    var_fi = (var as object).GetType().GetTypeInfo().DeclaredFields.ToList().FindAll(x => !x.IsStatic && x.IsPublic);
//                    var_pi = (var as object).GetType().GetTypeInfo().DeclaredProperties.ToList().FindAll(x => x.CanRead && x.CanWrite);
//                    uint count = (uint)(var_fi.Count + var_pi.Count);
//                    r += Sizes[count.GetType()];
//                    foreach (FieldInfo fi in var_fi)
//                    {
//                        r += GetLength(fi.GetValue(var));
//                    }
//                    foreach (PropertyInfo pi in var_pi)
//                    {
//                        r += GetLength(pi.GetValue(var));
//                    }

//                }
//            }
//            return r;
//        }

//        public dynamic Deserialize(byte[] data, Type type)
//        {
//            MemoryStream ms = new MemoryStream(data);
//            BinaryReader brms = new BinaryReader(ms);
//            dynamic r = Deserialize(brms, type);
//            brms.Dispose();
//            ms.Dispose();
//            return r;
//        }

//        public T Deserialize<T>(byte[] data)
//        {
//            MemoryStream ms = new MemoryStream(data);
//            BinaryReader brms = new BinaryReader(ms);
//            T r = Deserialize(brms, typeof(T));
//            brms.Dispose();
//            ms.Dispose();
//            return r;
//        }

//        public dynamic Deserialize(Stream data, Type type)
//        {
//            BinaryReader brms = new BinaryReader(data);
//            dynamic r = Deserialize(brms, type);
//            return r;
//        }

//        public T Deserialize<T>(Stream data)
//        {
//            BinaryReader brms = new BinaryReader(data);
//            T r = Deserialize(brms, typeof(T));
//            return r;
//        }

//        public dynamic Deserialize(BinaryReader brms, Type type)
//        {
//            bool isnullable = Nullable.GetUnderlyingType(type) != null;
//            dynamic r = type.GenerateEmptyObject();
//            bool isnull = brms.ReadBoolean();
//            if (!isnull)
//            {
//                Type[] generictypes = type.GetTypeInfo().GenericTypeArguments;
//                if (type.IsArray && type != typeof(byte[]))
//                {
//                    uint arankread = brms.ReadUInt32();
//                    int alength = (int)brms.ReadUInt32();
//                    r = Activator.CreateInstance(type, new object[] { alength });
//                    if (!Sizes.ContainsKey(type.GetElementType()))
//                    {
//                        for (int i0 = 0; i0 < alength; i0++)
//                        {
//                            r[i0] = Deserialize(brms, type.GetElementType());
//                        }
//                    }
//                    else
//                    {
//                        for (int i0 = 0; i0 < alength; i0++)
//                        {
//                            if (type.GetElementType() == typeof(bool))
//                            {
//                                r[i0] = brms.ReadBoolean();
//                            }
//                            else if (type.GetElementType() == typeof(byte))
//                            {
//                                r[i0] = brms.ReadByte();
//                            }
//                            else if (type.GetElementType() == typeof(sbyte))
//                            {
//                                r[i0] = brms.ReadSByte();
//                            }
//                            else if (type.GetElementType() == typeof(short))
//                            {
//                                r[i0] = brms.ReadInt16();
//                            }
//                            else if (type.GetElementType() == typeof(ushort))
//                            {
//                                r[i0] = brms.ReadUInt16();
//                            }
//                            else if (type.GetElementType() == typeof(int))
//                            {
//                                r[i0] = brms.ReadInt32();
//                            }
//                            else if (type.GetElementType() == typeof(uint))
//                            {
//                                r[i0] = brms.ReadUInt32();
//                            }
//                            else if (type.GetElementType() == typeof(long))
//                            {
//                                r[i0] = brms.ReadInt64();
//                            }
//                            else if (type.GetElementType() == typeof(ulong))
//                            {
//                                r[i0] = brms.ReadUInt64();
//                            }
//                            else if (type.GetElementType() == typeof(float))
//                            {
//                                r[i0] = brms.ReadSingle();
//                            }
//                            else if (type.GetElementType() == typeof(double))
//                            {
//                                r[i0] = brms.ReadDouble();
//                            }
//                        }
//                    }
//                }
//                else if (type == typeof(byte[]))
//                {
//                    int alength = (int)brms.ReadUInt32();
//                    r = brms.ReadBytes(alength);
//                }
//                else if (generictypes.Length == 1 && typeof(IEnumerable<>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
//                {
//                    int alength = (int)brms.ReadUInt32();
//                    ConstructorInfo constructor = type.GetTypeInfo().DeclaredConstructors.First(x => x.GetParameters().Length == 1 && x.GetParameters().All(y => y.ParameterType == typeof(IEnumerable<>).MakeGenericType(generictypes)));
//                    if (constructor == null)
//                    {
//                        r = null;
//                    }
//                    else
//                    {
//                        dynamic list = typeof(List<>).MakeGenericType(generictypes).GenerateEmptyObject();
//                        for (int i0 = 0; i0 < alength; i0++)
//                        {
//                            list.Add(Deserialize(brms, generictypes[0]));
//                        }
//                        r = constructor.Invoke(new object[] { list });
//                    }
//                }
//                else if (generictypes.Length == 2 && typeof(IReadOnlyDictionary<,>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
//                {
//                    int alength = (int)brms.ReadUInt32();
//                    ConstructorInfo constructor = type.GetTypeInfo().DeclaredConstructors.First(x => x.GetParameters().Length == 1 && x.GetParameters().All(y => y.ParameterType == typeof(IDictionary<,>).MakeGenericType(generictypes)));
//                    if (constructor == null)
//                    {
//                        r = null;
//                    }
//                    else
//                    {
//                        dynamic dictionary = typeof(Dictionary<,>).MakeGenericType(generictypes).GenerateEmptyObject();
//                        for (int i0 = 0; i0 < alength; i0++)
//                        {
//                            dynamic pair = Deserialize(brms, typeof(KeyValuePair<,>).MakeGenericType(generictypes));
//                            dictionary.Add(pair.Key, pair.Value);
//                        }
//                        r = constructor.Invoke(new object[] { dictionary });
//                    }
//                }
//                else if (generictypes.Length == 2 && typeof(KeyValuePair<,>).MakeGenericType(generictypes).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
//                {
//                    ConstructorInfo constructor = type.GetTypeInfo().DeclaredConstructors.First(x => x.GetParameters().Length == 2 && x.GetParameters()[0].ParameterType == generictypes[0] && x.GetParameters()[1].ParameterType == generictypes[1]);
//                    object key = Deserialize(brms, generictypes[0]);
//                    object value = Deserialize(brms, generictypes[1]);
//                    r = constructor.Invoke(new object[] { key, value });

//                }
//                else if (Sizes.ContainsKey(type) || (isnullable && Sizes.ContainsKey(Nullable.GetUnderlyingType(type))))
//                {
//                    if (isnullable)
//                    {
//                        type = Nullable.GetUnderlyingType(type);
//                    }

//                    if (type == typeof(bool))
//                    {
//                        r = brms.ReadBoolean();
//                    }
//                    else if (type == typeof(byte))
//                    {
//                        r = brms.ReadByte();
//                    }
//                    else if (type == typeof(sbyte))
//                    {
//                        r = brms.ReadSByte();
//                    }
//                    else if (type == typeof(short))
//                    {
//                        r = brms.ReadInt16();
//                    }
//                    else if (type == typeof(ushort))
//                    {
//                        r = brms.ReadUInt16();
//                    }
//                    else if (type == typeof(int))
//                    {
//                        r = brms.ReadInt32();
//                    }
//                    else if (type == typeof(uint))
//                    {
//                        r = brms.ReadUInt32();
//                    }
//                    else if (type == typeof(long))
//                    {
//                        r = brms.ReadInt64();
//                    }
//                    else if (type == typeof(ulong))
//                    {
//                        r = brms.ReadUInt64();
//                    }
//                    else if (type == typeof(float))
//                    {
//                        r = brms.ReadSingle();
//                    }
//                    else if (type == typeof(double))
//                    {
//                        r = brms.ReadDouble();
//                    }
//                }
//                else if (r is char)
//                {
//                    int length = brms.ReadInt32();
//                    byte[] chr = brms.ReadBytes(length);
//                    r = TextEncoding.GetString(chr, 0, length)[0];
//                }
//                else if (r is string)
//                {
//                    int length = brms.ReadInt32();
//                    byte[] str = brms.ReadBytes(length);
//                    r = TextEncoding.GetString(str, 0, length);
//                }
//                else if (r is Guid)
//                {
//                    r = new Guid(brms.ReadBytes(16));
//                }
//                else if (r is DateTime)
//                {
//                    r = DateTime.FromBinary(brms.ReadInt64());
//                }
//                else if (type.GetTypeInfo().IsEnum)
//                {
//                    r = Enum.ToObject(type, Deserialize(brms, Enum.GetUnderlyingType(type)));
//                }
//                else if (type.IsAnonymousType())
//                {
//                    List<PropertyInfo> var_pi;
//                    var_pi = type.GetTypeInfo().DeclaredProperties.ToList().FindAll(x => x.CanRead == true);
//                    uint count = brms.ReadUInt32();
//                    object[] prms = new object[var_pi.Count];
//                    for (int i0 = 0; i0 < var_pi.Count; i0++)
//                    {
//                        prms[i0] = Deserialize(brms, var_pi[i0].PropertyType);
//                    }
//                    r = Activator.CreateInstance(type, prms);
//                }
//                else
//                {
//                    //Обработка структур. Не мешает обработке классов, однако, помогает поменять поля в структуре
//                    object obj = r;
//                    List<FieldInfo> var_fi;
//                    List<PropertyInfo> var_pi;
//                    var_fi = type.GetTypeInfo().DeclaredFields.ToList().FindAll(x => !x.IsStatic && x.IsPublic);
//                    var_pi = type.GetTypeInfo().DeclaredProperties.ToList().FindAll(x => x.CanRead && x.CanWrite);
//                    uint count = brms.ReadUInt32();
//                    foreach (FieldInfo fi in var_fi)
//                    {
//                        fi.SetValue(obj, Deserialize(brms, fi.FieldType));
//                    }
//                    foreach (PropertyInfo pi in var_pi)
//                    {
//                        pi.SetValue(obj, Deserialize(brms, pi.PropertyType));
//                    }
//                    //Возвращаем объект обратно в переменную возврата
//                    r = obj;
//                }
//            }
//            else if (type.CanBeNull())
//            {
//                r = null;
//            }
//            return r;
//        }

//        public void RemoveData(ref byte[] message, dynamic data)
//        {
//            int k = GetLength(data);
//            Array.Reverse(message);
//            Array.Resize(ref message, message.Length - k);
//            Array.Reverse(message);

//        }

//        public void AddData(ref byte[] message, dynamic data)
//        {
//            byte[] l = Serialize(data);
//            int k = l.Length;
//            Array.Resize(ref l, message.Length + l.Length);
//            Array.Copy(message, 0, l, k, message.Length);
//            message = l;
//        }
//    }
//}
