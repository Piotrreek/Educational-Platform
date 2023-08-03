import { Outlet } from "react-router-dom";
import Header from "../components/ui/Header";
import Footer from "../components/ui/Footer";
import AuthVerify from "../common/AuthVerify";
import useAuth from "../hooks/useAuth";

const Layout = () => {
  const { logout } = useAuth();
  return (
    <>
      <AuthVerify logout={logout} />
      <Header />
      <main>
        <Outlet />
      </main>
      <Footer />
    </>
  );
};

export default Layout;
