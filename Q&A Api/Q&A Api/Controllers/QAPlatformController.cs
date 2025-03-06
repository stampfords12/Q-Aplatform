using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Q_A_Api.Const;
using Q_A_Api.Core;
using Q_A_Api.Models;
using System.Data;
using System.Data.Common;
using System.Linq;
using static Q_A_Api.Core.RespDataModels;

namespace Q_A_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QAPlatformController : ControllerBase
    {
        private readonly string connectionString = @"server=(local)\SQLExpress;database=Northwind;integrated Security=SSPI;";
        private readonly ILogger<QAPlatformController> _logger;
        public QAPlatformController(ILogger<QAPlatformController> logger)
        {
            _logger = logger;
        }

        #region Overview
        // Api space
        // #1 - โพสคำถามและคำตอบ อยู่ถายในเส้นเดียวกัน ส่ง Mode กำกับ
        #endregion

        [HttpPost(Name = "InputData")]
        public ActionResult<bool> QuestionAnsAnswers([FromBody] ReqQuestionAnswersModels models)
        {
            bool response = true;

            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    switch (models.Mode)
                    {
                        case QAPlatformConst.Mode.EDIT:
                        case QAPlatformConst.Mode.QUESTION:
                            if (models.QuestionId == null)
                            {
                                string query = "INSERT INTO QuestionAnswer (Ref_QuestionId, Type, CustomerId, Tx_Value, Create_Dt,  Update_Dt, Point, Tag)";
                                query += " VALUES (@Ref_QuestionId, @Type, @CustomerId, @Tx_Value, @Create_Dt, @Update_Dt, @Point, @Tag)";

                                SqlCommand myCommand = new SqlCommand(query, _con);
                                myCommand.Parameters.AddWithValue("@Ref_QuestionId", models.Ref_QuestionId);
                                myCommand.Parameters.AddWithValue("@Type", models.Mode);
                                myCommand.Parameters.AddWithValue("@CustomerId", models.Customer_Id);
                                myCommand.Parameters.AddWithValue("@Tx_Value", models.Question);
                                myCommand.Parameters.AddWithValue("@Create_Dt", models.Created_Date);
                                myCommand.Parameters.AddWithValue("@Update_Dt", models.Update_Date);
                                myCommand.Parameters.AddWithValue("@Point", 0);
                                myCommand.Parameters.AddWithValue("@Tag", models.Tag);
                                myCommand.ExecuteNonQuery();
                            }
                            else
                            {
                                string query = "UPDATE QuestionAnswer SET " +
                                    " Ref_QuestionId = @Ref_QuestionId, " +
                                    " Type = @Type, " +
                                    " CustomerId = @CustomerId, " +
                                    " Tx_Value = @Tx_Value, " +
                                    " Create_Dt = @Create_Dt, " +
                                    " Update_Dt = @Update_Dt, " +
                                    " Point = @Point, " +
                                    " Tag = @Tag";
                                query += " WHERE QuestionId = ";

                                SqlCommand myCommand = new SqlCommand(query, _con);
                                myCommand.Parameters.AddWithValue("@Ref_QuestionId", models.Ref_QuestionId);
                                myCommand.Parameters.AddWithValue("@Type", models.Mode);
                                myCommand.Parameters.AddWithValue("@CustomerId", models.Customer_Id);
                                myCommand.Parameters.AddWithValue("@Tx_Value", models.Question);
                                myCommand.Parameters.AddWithValue("@Create_Dt", models.Created_Date);
                                myCommand.Parameters.AddWithValue("@Update_Dt", models.Update_Date);
                                myCommand.Parameters.AddWithValue("@Point", 0);
                                myCommand.Parameters.AddWithValue("@Tag", models.Tag);
                                myCommand.ExecuteNonQuery();
                            }
                            break;
                        case QAPlatformConst.Mode.ANSWER:
                            break;
                        case QAPlatformConst.Mode.REPLY:
                            break;
                        default:
                            throw new Exception("ข้อมูล mode ไม่ถูกต้อง");
                    }
                }                
            }
            catch (Exception ex)
            {
                response = false;
            }

            return response;
        }

        [HttpPost(Name = "Search")]
        public ActionResult<RespDataModels> Search([FromBody] ReqFilterModels filter)
        {
            RespDataModels response = new RespDataModels();
            List<string> queryStatement = new List<string>();

            try
            {
                using (SqlConnection _con = new SqlConnection(connectionString))
                {
                    queryStatement.Add(" SELECT * FROM QuestionAnswer ");

                    if (filter.Filter_Models != null)
                    {
                        queryStatement.Add($"{Environment.NewLine} + WHERE ");

                        List<string> listwhere = new List<string>();
                        filter.Filter_Models = filter.Filter_Models.OrderBy(s => s.Seq).ToList();
                        foreach (var where in filter.Filter_Models)
                        {
                            switch (where.Key)
                            {
                                case "tag":
                                    listwhere.Add($"{QuestionAnswersModels.Tag} = {where.Value}");
                                    break;
                                case "datetime":
                                    listwhere.Add($"{QuestionAnswersModels.Create_Dt} = {where.Value}");
                                    break;
                                case "point":
                                    listwhere.Add($"{QuestionAnswersModels.Point} = {where.Value}");
                                    break;
                                default:
                                    break;
                            }
                        }
                        string condition = string.Join($"{Environment.NewLine} + AND ", listwhere);
                        queryStatement.Add(condition);
                    }

                    if (filter.Order_Models != null)
                    {
                        queryStatement.Add($"{Environment.NewLine} + ORDER BY ");

                        List<string> listorder = new List<string>();
                        filter.Order_Models = filter.Order_Models.OrderBy(s => s.Seq).ToList();
                        foreach (var order in filter.Order_Models)
                        {
                            switch (order.Key)
                            {
                                case "id":
                                    listorder.Add($"{QuestionAnswersModels.QuestionId} {order.Sort}");
                                    break;
                                case "update_date":
                                    listorder.Add($"{QuestionAnswersModels.Update_Dt} {order.Sort}");
                                    break;
                                case "create_date":
                                    listorder.Add($"{QuestionAnswersModels.Create_Dt} {order.Sort}");
                                    break;
                                case "point":
                                    listorder.Add($"{QuestionAnswersModels.Point} {order.Sort}");
                                    break;
                                default:
                                    break;
                            }
                        }
                        string condition = string.Join($"{Environment.NewLine} + AND ", listorder);
                        queryStatement.Add(condition);
                    }

                    string query = string.Join(" ", queryStatement);
                    using (SqlCommand _cmd = new SqlCommand(query, _con))
                    {
                        DataTable questionTable = new DataTable("Search");
                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);

                        _con.Open();
                        _dap.Fill(questionTable);
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
