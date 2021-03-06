﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Util
{
   ///<summary><![CDATA[加密解密帮助类]]></summary> 
    
    public class DesHelper     
    { 
        /// <summary>
        /// 字符串加密函数
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="key">加密key</param>
        /// <returns></returns>
        public static string Encode(string str, string key) 
                
        { 
                    
            try                         
            {  
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();                             
                provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));                             
                provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));                             
                byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(str); 
                                
                MemoryStream stream = new MemoryStream();                              
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(),CryptoStreamMode.Write);  
                                
                stream2.Write(bytes, 0, bytes.Length);                             
                stream2.FlushFinalBlock();
                StringBuilder builder = new StringBuilder();                            
                foreach (byte num in stream.ToArray()) 
                                
                {
                    builder.AppendFormat("{0:X2}", num);                             
                }                              
                stream.Close();                              
                return builder.ToString().ToLower();  
                        
            }                      
                catch (Exception) { return "xxxx"; }                 
        }                  
        /// <summary>
        /// 字符串解密函数
        /// </summary>
        /// <param name="str">解密字符串</param>
        /// <param name="key">解密key</param>
        /// <returns></returns>
        public static string Decode(string str, string key) 
                
        {                 
            try               
            {                    
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();                             
                provider.Key = Encoding.ASCII.GetBytes(key.Substring(0, 8));                             
                provider.IV = Encoding.ASCII.GetBytes(key.Substring(0, 8));                             
                byte[] buffer = new byte[str.Length / 2];                             
                for (int i = 0; i < (str.Length / 2); i++)                             
                {                                  
                    int num2 = Convert.ToInt32(str.Substring(i * 2, 2), 0x10);                                     
                    buffer[i] = (byte)num2;                             
                }                              
                MemoryStream stream = new MemoryStream();                             
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(),CryptoStreamMode.Write);                              
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();                             
                stream.Close();                              
                return Encoding.GetEncoding("GB2312").GetString(stream.ToArray());                     
            }
                catch (Exception) { return ""; }             
            }             
        }     
}


/*
JAVADES 加密解密类
  
package com.bgxt.messages;  
import java.io.UnsupportedEncodingException; 
import java.security.*; 
import javax.crypto.Cipher; 
import javax.crypto.SecretKey; 
import javax.crypto.SecretKeyFactory; 
import javax.crypto.spec.DESKeySpec;  
import javax.crypto.spec.IvParameterSpec; 
 * /
/**  
* 
字符串工具集合
  
* @author Liudong 
*/  
/*
    public class StringUtils { 
  
    private static final String PASSWORD_CRYPT_KEY = XmlUtil.getConfig().getPasswdKey().substring(0,8);  
    //private final static String DES = "DES";  
    //private static final byte[] desKey;   
    //解密数据
     
    public static String decrypt(String message,String key) throws Exception {    
              
            
    byte[] bytesrc =convertHexString(message);        
            
    Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");        
            
    DESKeySpec desKeySpec = new DESKeySpec(key.getBytes("UTF-8"));       
            
    SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("DES");       
            
    SecretKey secretKey = keyFactory.generateSecret(desKeySpec);       
            
    IvParameterSpec iv = new IvParameterSpec(key.getBytes("UTF-8"));    
            
    cipher.init(Cipher.DECRYPT_MODE, secretKey, iv);           
            
    byte[] retByte = cipher.doFinal(bytesrc);         
            
    return new String(retByte);     
    }    
    public static byte[] encrypt(String message, String key)    
            
    throws Exception {     
        
    Cipher cipher = Cipher.getInstance("DES/CBC/PKCS5Padding");   
        
    DESKeySpec desKeySpec = new DESKeySpec(key.getBytes("UTF-8"));   
        
    SecretKeyFactory keyFactory = SecretKeyFactory.getInstance("DES");    
        
    SecretKey secretKey = keyFactory.generateSecret(desKeySpec);    
        
    IvParameterSpec iv = new IvParameterSpec(key.getBytes("UTF-8"));    
        
    cipher.init(Cipher.ENCRYPT_MODE, secretKey, iv);   
        
    return cipher.doFinal(message.getBytes("UTF-8"));    
    }    
    public static String encrypt(String value){ 
    String result=""; 
    try{  
    value=java.net.URLEncoder.encode(value, "utf-8");   
    result=toHexString(encrypt(value, PASSWORD_CRYPT_KEY)).toUpperCase(); 
    }catch(Exception ex){ 
       
    ex.printStackTrace(); 
       
    return ""; 
    }  
    return result;  
    }  
    public static byte[] convertHexString(String ss)     
    {      
    byte digest[] = new byte[ss.length() / 2];     
    for(int i = 0; i < digest.length; i++)     
    {      
    String byteString = ss.substring(2 * i, 2 * i + 2);     
    int byteValue = Integer.parseInt(byteString, 16);     
    digest[i] = (byte)byteValue;      
    }      
    return digest;     
    }     
    public static String toHexString(byte b[]) {    
    
    StringBuffer hexString = new StringBuffer();    
    
    for (int i = 0; i < b.length; i++) {     
        
    String plainText = Integer.toHexString(0xff & b[i]);    
        
    if (plainText.length() < 2)    
                
        plainText = "0" + plainText;    
            
    hexString.append(plainText);    
    
    }        
    
        return hexString.toString();    
    }     
    public static void main(String[] args) throws Exception {    
        
        String value="01";     
            
        System.out.println("加密数据:"+value);   
            
        System.out.println("密码为:"+XmlUtil.getConfig().getPasswdKey());  
            
        String a=encrypt( value);     
            
        System.out.println("加密后的数据为:"+a);    
    }    
} 
*/