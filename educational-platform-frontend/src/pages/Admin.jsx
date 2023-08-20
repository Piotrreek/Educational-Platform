import { Outlet } from "react-router";
import AdminActions from "../components/admin/AdminActions";
import AdminContainer from "../components/admin/AdminContainer";
import AdminPanelHeader from "../components/admin/AdminPanelHeader";

const Admin = () => {
  return (
    <AdminContainer>
      <AdminPanelHeader />
      <AdminActions />
      <Outlet />
    </AdminContainer>
  );
};

export default Admin;
