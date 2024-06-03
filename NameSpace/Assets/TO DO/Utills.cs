using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;

public class Utills
{

    public static void Reflect(object reflectedObject, BindingFlags bindingFlags, string prefix = "")
    {
        Type type = reflectedObject.GetType();
        if (prefix == string.Empty)
        {
            prefix += reflectedObject.GetType();
        }
        PropertyInfo[] prpertyInfomation = type.GetProperties(bindingFlags);
        for (int i = 0; i < prpertyInfomation.Length; i++)
        {
            string rwPermitions = "";
            if (prpertyInfomation[i].CanRead && prpertyInfomation[i].CanWrite)
            {
                rwPermitions = "rw";
            }
            else if (prpertyInfomation[i].CanRead && !prpertyInfomation[i].CanWrite)
            {
                rwPermitions = "ro";
            }
            else if (!prpertyInfomation[i].CanRead && prpertyInfomation[i].CanWrite)
            {
                rwPermitions = "wo";
            }
            else if (!prpertyInfomation[i].CanRead && !prpertyInfomation[i].CanWrite)
            {
                rwPermitions = "none";
            }
            Debug.Log($"<b>{prefix}:</b> {prpertyInfomation[i].Name}, type: {prpertyInfomation[i].PropertyType}, value: {prpertyInfomation[i].GetValue(reflectedObject)}, readWritePermitions: {rwPermitions}, GetMethod: {prpertyInfomation[i].GetMethod}, SetMethod: {prpertyInfomation[i].SetMethod}");
            if (DoesNeedReflection(prpertyInfomation[i].PropertyType))
            {
                prefix += "->" + prpertyInfomation[i].Name;
                if (prpertyInfomation[i].GetValue(reflectedObject) == null)
                {
                    Debug.LogWarning($"<b>{prefix}:</b> {prpertyInfomation[i].Name} Is set to null. The branch will be stoped.");
                    continue;
                }
                Reflect(prpertyInfomation[i].GetValue(reflectedObject), bindingFlags, prefix);
            }
        }
        FieldInfo[] fieldInfomation = type.GetFields(bindingFlags);
        for (int i = 0; i < fieldInfomation.Length; i++)
        {
            Debug.Log($"<b>{prefix}:</b> {fieldInfomation[i].Name}, type: {fieldInfomation[i].FieldType}, value: {fieldInfomation[i].GetValue(reflectedObject)}, isPublic: {fieldInfomation[i].IsPublic}, isPrivate: {fieldInfomation[i].IsPrivate}, isStatic: {fieldInfomation[i].IsStatic}");
            if (DoesNeedReflection(fieldInfomation[i].FieldType))
            {
                prefix += "->" + fieldInfomation[i].Name;
                if (fieldInfomation[i].GetValue(reflectedObject) == null)
                {
                    Debug.LogWarning($"<b>{prefix}:</b> {fieldInfomation[i].Name} Is set to null. The branch will be stoped.");
                    continue;
                }
                Reflect(fieldInfomation[i].GetValue(reflectedObject), bindingFlags, prefix);
            }
        }


        bool DoesNeedReflection(Type testedType)
        {
            if (TestVsNormalTypes() || IsTypeIEnumeratbleType())
            {
                if (testedType == type)
                {
                    Debug.LogWarning($"<b>{prefix}:</b> {testedType} has a veriable of same type. The branch will be stoped");
                }
                return false;
            }
            return true;


            bool TestVsNormalTypes()
            {
                return testedType == typeof(byte) || testedType == typeof(short) || testedType == typeof(int) || testedType == typeof(long) || testedType == typeof(IntPtr) || testedType == typeof(sbyte) || testedType == typeof(ushort) || testedType == typeof(uint) || testedType == typeof(ulong) | testedType == typeof(UIntPtr) || testedType == typeof(float) || testedType == typeof(double) || testedType == typeof(decimal) || testedType == typeof(bool) || testedType == typeof(char) || testedType == typeof(string) || testedType == typeof(Type) || testedType == typeof(Assembly) || testedType == type;
            }
            bool IsTypeIEnumeratbleType()
            {
                return DoesContainInterface(typeof(IEnumerable<byte>)) || DoesContainInterface(typeof(IEnumerable<short>)) || DoesContainInterface(typeof(IEnumerable<int>)) || DoesContainInterface(typeof(IEnumerable<long>)) || DoesContainInterface(typeof(IEnumerable<IntPtr>)) || DoesContainInterface(typeof(IEnumerable<sbyte>)) || DoesContainInterface(typeof(IEnumerable<ushort>)) || DoesContainInterface(typeof(IEnumerable<uint>)) || DoesContainInterface(typeof(IEnumerable<ulong>)) || DoesContainInterface(typeof(IEnumerable<UIntPtr>)) || DoesContainInterface(typeof(IEnumerable<float>)) || DoesContainInterface(typeof(IEnumerable<double>)) || DoesContainInterface(typeof(IEnumerable<decimal>)) || DoesContainInterface(typeof(IEnumerable<bool>)) || DoesContainInterface(typeof(IEnumerable<char>)) || DoesContainInterface(typeof(IEnumerable<string>)) || DoesContainInterface(typeof(IEnumerable<Type>)) || DoesContainInterface(typeof(IEnumerable<Assembly>));
            }
            bool DoesContainInterface(Type interfaceType)
            {
                return testedType.GetInterfaces().Contains(interfaceType);
            }
        }
    }

    public static void ConvertDaysToYear_Months_DayOfMonth(int days, out int year, out int month, out int dayOfTheMonth)
    {
        year = 0;
        bool canAddMoreYears = true;
        bool isCurrentYearLeapYear = false;

        int numberOfDaysLeftInYears = days;
        int i = 0;
        while (canAddMoreYears)
        {
            if (numberOfDaysLeftInYears > 365)
            {
                isCurrentYearLeapYear = false;
                numberOfDaysLeftInYears -= 365;
                i++;
                year++;
                if (i % 4 == 0 && i % 400 != 0) // Check for leap year
                {
                    numberOfDaysLeftInYears--;
                    isCurrentYearLeapYear = true;
                }
            }
            else
            {
                canAddMoreYears = false;
            }
        }

        numberOfDaysLeftInYears += isCurrentYearLeapYear ? 1 : 0;
        month = 0;
        for (i = 0; i < 12; i++)
        {
            month = i;
            switch (i)
            {
                case 0: // January
                    if (numberOfDaysLeftInYears > 31)
                    {
                        numberOfDaysLeftInYears -= 31;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 1: // February
                    if (numberOfDaysLeftInYears > (isCurrentYearLeapYear ? 29 : 28))
                    {
                        numberOfDaysLeftInYears -= (isCurrentYearLeapYear ? 29 : 28);
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 2: // March
                    if (numberOfDaysLeftInYears > 31)
                    {
                        numberOfDaysLeftInYears -= 31;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 3: // April
                    if (numberOfDaysLeftInYears > 30)
                    {
                        numberOfDaysLeftInYears -= 30;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 4: // May
                    if (numberOfDaysLeftInYears > 31)
                    {
                        numberOfDaysLeftInYears -= 31;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 5: // June
                    if (numberOfDaysLeftInYears > 30)
                    {
                        numberOfDaysLeftInYears -= 30;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 6: // July
                    if (numberOfDaysLeftInYears > 31)
                    {
                        numberOfDaysLeftInYears -= 31;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 7: // August
                    if (numberOfDaysLeftInYears > 31)
                    {
                        numberOfDaysLeftInYears -= 31;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 8: // September
                    if (numberOfDaysLeftInYears > 30)
                    {
                        numberOfDaysLeftInYears -= 30;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 9: // October
                    if (numberOfDaysLeftInYears > 31)
                    {
                        numberOfDaysLeftInYears -= 31;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 10: // November
                    if (numberOfDaysLeftInYears > 30)
                    {
                        numberOfDaysLeftInYears -= 30;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
                case 11: // December
                    if (numberOfDaysLeftInYears > 31)
                    {
                        numberOfDaysLeftInYears -= 31;
                        continue;
                    }
                    dayOfTheMonth = numberOfDaysLeftInYears;
                    return;
            }
        }
        dayOfTheMonth = -1;
    }




}
