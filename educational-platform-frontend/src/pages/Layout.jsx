import { Outlet } from "react-router-dom";

import Header from "../components/ui/Header";
import Footer from "../components/ui/Footer";

const Layout = () => {
  return (
    <>
      <Header />
      <main>
        <Outlet />
      </main>
      <Footer />
    </>
  );
};

export default Layout;
